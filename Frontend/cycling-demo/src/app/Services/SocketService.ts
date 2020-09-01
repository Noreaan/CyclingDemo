import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import * as signalR from '@Microsoft/signalr';
import { Interface } from 'readline';

@Injectable({
    providedIn: 'root'
})

export class SocketService {
    private connection: any = new signalR.HubConnectionBuilder()
        .withUrl('https://localhost:44391/updatesocket')
        .configureLogging(signalR.LogLevel.Information)
        .build();

    private socketUpdateRiderCountObject = new Subject<any>();
    private socketUpdateListObject = new Subject<any>();

    constructor() {
    }
    
    public async start() {
        try {
            this.connection.onclose(async () => {
                await this.start();
            });
    
            this.connection.on('updateRiderCount', (updateRiders) => { this.socketUpdateRiderCountObject.next(updateRiders); });
            this.connection.on('listUpdated', () => { this.socketUpdateListObject.next(); });
    
            await this.connection.start();
            this.broadcastConnect();
        } catch (err) {
            setTimeout(() => this.start(), 5000);
        }
    }

    public broadcastConnect() {
        this.connection.invoke("OnConnect").catch(err => console.error(err));
    }

    public broadcastRiderCountUpdate() {
        this.connection.invoke("updateRiderCount").catch(err => console.error(err));
    }

    public joinGroup(tab: string) {
        this.connection.invoke("joinGroup", tab).catch(err => console.error(err));
    }

    public leaveGroup(tab: string) {
        this.connection.invoke("leaveGroup", tab).catch(err => console.error(err));
    }

    public alertGroupListUpdated(tab: string) {
        this.connection.invoke("alertGroupListUpdated", tab).catch(err => console.error(err));
    }

    public getRiderCountUpdate(){
        return this.socketUpdateRiderCountObject.asObservable();
    }

    public getUpdateList(){
        return this.socketUpdateListObject.asObservable();
    }
}
