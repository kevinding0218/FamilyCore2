import { Injectable } from '@angular/core';
import { Subject } from "rxjs/Subject";
import { BrowserXhr } from "@angular/http";

@Injectable()
export class ProgressService {
    //Subject: a class that derives from Observable
    //that has all the characters sticks to the observable
    //and also give us ability to push a new value into that observable
    //uploadProgress: Subject<any> = new Subject();
    //downloadProgress: Subject<any> = new Subject();
    private uploadProgress: Subject<any>;

    startTracking() {
        this.uploadProgress = new Subject();
        return this.uploadProgress;
    }

    notify(progress) {
        //console.log("BEFORE", progress);
        if (this.uploadProgress)
            this.uploadProgress.next(progress);
        //console.log("AFTER", progress);
    }

    endTracking() {
        if (this.uploadProgress)
            this.uploadProgress.complete();
    }
}

@Injectable()
export class BrowserXhrWithProgress extends BrowserXhr {
    constructor(private service: ProgressService) {
        super();
    }

    build(): XMLHttpRequest {
        var xhr: XMLHttpRequest = super.build();

        /*
        xhr.onprogress = (event) => {
            this.service.downloadProgress.next(this.createProgress(event));
        };
        */

        xhr.upload.onprogress = (event) => {
            //this.service.uploadProgress.next(this.createProgress(event));
            this.service.notify(this.createProgress(event));
        };

        xhr.upload.onloadend = () => {
            //console.log("BEFORE", this.service.uploadProgress);
            this.service.endTracking();
            //console.log("AFTER", this.service.uploadProgress);
        }

        return xhr;
    }

    private createProgress(event) {
        return {
            total: event.total,
            percentage: Math.round(event.loaded / event.total * 100)
        };
    }
}

//XMLHttpRequest
//BrowserXhr