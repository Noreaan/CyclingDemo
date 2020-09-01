import { Component } from '@angular/core';
import { MenuItem, MessageService } from 'primeng/api';
import { RiderService } from './Services/RiderService';
import { SocketService } from './Services/SocketService';

interface IRiders {
    firstname: string;
    lastname: string;
    age: string;
    team: string;
}

interface IRidersCount {
    tourRiders: number;
    parisRiders: number;
}

enum Tab {
    Tour = 1,
    Paris = 2
}

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
    providers: [MessageService, SocketService]
})
export class AppComponent {
    items: MenuItem[];
    activeItem: MenuItem;
    cols: any[];
    ridersTour: IRiders[];
    ridersParis: IRiders[];
    ridersCount: IRidersCount = { tourRiders: 0, parisRiders: 0 } as IRidersCount;

    constructor(
        public messageService: MessageService,
        public riderService: RiderService,
        public socketService: SocketService
    ) {
    }

    ngOnInit() {
        this.updateMenu();

        this.setCols();
        this.initSocketAndLoadData();
    }

    initSocketAndLoadData() {
        this.socketService.start().then(() => {
            this.loadTourRiders()

            this.socketService.getRiderCountUpdate().subscribe(
                (res: IRidersCount) => {
                    this.ridersCount = res;
                    this.updateMenu();
                }
            );

            this.socketService.getUpdateList().subscribe(
                () => {
                    this.messageService.add({ severity: 'info', summary: 'New Data', detail: 'The rider board has been updated. Refreshing riders...' });

                    if (this.activeItem.id == Tab.Tour.toString()) {
                        this.loadTourRiders();
                    } else {
                        this.loadParisRiders();
                    }
                }
            );
        });
    }

    loadTourRiders() {
        this.riderService.getTourRiders().then((response) => {
            this.ridersTour = response as IRiders[];
            this.socketService.leaveGroup(Tab.Paris);
            this.socketService.joinGroup(Tab.Tour);
        })
            .catch(() => {
                this.messageService.add({ severity: 'warn', summary: 'Warning', detail: 'Could not load tour riders' });
            });
    }

    loadParisRiders() {
        this.riderService.getParisRiders().then((response) => {
            this.ridersParis = response as IRiders[];
            this.socketService.leaveGroup(Tab.Tour);
            this.socketService.joinGroup(Tab.Paris);
        })
            .catch(() => {
                this.messageService.add({ severity: 'warn', summary: 'Warning', detail: 'Could not load paris riders' });
            });
    }

    tourClick() {
        this.riderService.addTourRiders().then((response) => {
            this.ridersTour = response as IRiders[];
            this.messageService.add({ severity: 'success', summary: 'Success', detail: 'New rider add for Tour de France' });
            this.socketService.broadcastRiderCountUpdate();
            this.socketService.alertGroupListUpdated(Tab.Tour);
        })
            .catch(() => {
                this.messageService.add({ severity: 'warn', summary: 'Warning', detail: 'Could not add rider for Tour de France' });
            });
    }

    parisClick() {
        this.riderService.addParisRiders().then((response) => {
            this.ridersParis = response as IRiders[];
            this.messageService.add({ severity: 'success', summary: 'Success', detail: 'New rider add for Paris Roubaix' });
            this.socketService.broadcastRiderCountUpdate();
            this.socketService.alertGroupListUpdated(Tab.Paris);
        })
            .catch(() => {
                this.messageService.add({ severity: 'warn', summary: 'Warning', detail: 'Could not add rider for Paris Roubaix' });
            });
    }

    // Dit kan veel beter geschreven worden aangezien!
    updateMenu() {
        let prev = this.activeItem;
        this.items = [
            { id: Tab.Tour.toString(), label: `Tour de France ${this.ridersCount.tourRiders}`, icon: 'pi pi-fw pi-home', command: (event) => { this.activeItem = this.items[0];  this.loadTourRiders() } },
            { id: Tab.Paris.toString(), label: `Paris Roubaix ${this.ridersCount.parisRiders}`, icon: 'pi pi-fw pi-calendar', command: (event) => { this.activeItem = this.items[1]; this.loadParisRiders() } }
        ];
        this.activeItem = prev ? this.items.find(x => x.id == prev.id) : this.items[0];
    }

    setCols() {
        this.cols = [
            { field: 'firstname', header: 'Firstname' },
            { field: 'lastname', header: 'Lastname' },
            { field: 'age', header: 'Age' },
            { field: 'team', header: 'Team' }
        ];
    }
}
