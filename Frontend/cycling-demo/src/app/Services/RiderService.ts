import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})

export class RiderService {
    constructor(private http: HttpClient) {
    }

    public getTourRiders(): Promise<Object> {
        return this.http.get('https://localhost:44391/riders/tour').toPromise();
    }

    public getParisRiders(): Promise<Object> {
        return this.http.get('https://localhost:44391/riders/paris').toPromise();
    }

    public addParisRiders(): Promise<Object> {
        return this.http.get('https://localhost:44391/riders/add/paris').toPromise();
    }

    public addTourRiders(): Promise<Object> {
        return this.http.get('https://localhost:44391/riders/add/tour').toPromise();
    }
}
