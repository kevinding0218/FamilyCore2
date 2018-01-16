import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class ErrorLogService {
  constructor() {
  }

  logError(error: any) {
    const date = new Date().toISOString();
    if (error instanceof HttpErrorResponse) {
      console.error(date, 'There was an HTTP error.', error.message, 'Status code:', (<HttpErrorResponse>error).status);
    } else if (error instanceof TypeError) {
      console.error(date, 'There was a Type error.', error.message);
    } else if (error instanceof Error) {
      console.error(date, 'There was a general error.', error.message);
    } else {
      if (error.status == 400) {
        let validationErrorDictionary = JSON.parse(error._body);
        for (var fieldName in validationErrorDictionary) {
          if (validationErrorDictionary.hasOwnProperty(fieldName)) {
            error.message = validationErrorDictionary[fieldName];
          }
        }
      }
      console.error(date, 'Nobody threw an Error but something happened!', error.message);
    }
  }
}