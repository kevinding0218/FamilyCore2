import { Injectable } from '@angular/core';
import { Http } from "@angular/http";

@Injectable()
export class EntreePhotoUploadService {
    private readonly apiPort: string = localStorage.getItem('WebApiPath').toString();
    private readonly apiEndPoint: string = this.apiPort + '/entreePhoto/';
    constructor(private http: Http) {

    }

    upload(entreeId, photo) {
        var formData = new FormData();
        formData.append('file', photo);
        //return this.http.post('/api/vehicles/' + vehicleId + '/photos', formData);
        return this.http.post(this.apiEndPoint + `${entreeId}/photos`, formData)
            .map(res => res.json());
    }

    getPhotos(entreeId) {
        return this.http.get(this.apiEndPoint + `${entreeId}/photos`)
            .map(res => res.json());
    }
}