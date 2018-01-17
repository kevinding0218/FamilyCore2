import { ErrorHandler, Inject, Injector, NgZone, isDevMode } from "@angular/core";
import { Response } from "@angular/http";
import { ToastrService } from "ngx-toastr";
import { ErrorLogService } from "../services/event/error.log.service";

// import * as Raven from 'raven-js'

export class AppErrorHandler implements ErrorHandler {
    constructor(
        @Inject(NgZone) private ngZone: NgZone,
        @Inject(Injector) private injector: Injector,
        @Inject(ErrorLogService) private errorLogService: ErrorLogService
    ) { }

    private get toastr(): ToastrService {
        return this.injector.get(ToastrService);
    }

    public handleError(error: any): void {
        this.ngZone.run(() => {
            this.errorLogService.logError(error);

            if (typeof error.message != 'undefined') {
                this.toastr.error(
                    error.message,
                    'Error'
                );
            } else {
                this.toastr.error(
                    'Check if Api server is turned on or in good state',
                    'Error'
                );
            }
        });

        //throw error;
    }
}