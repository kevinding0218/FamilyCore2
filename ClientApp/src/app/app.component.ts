import { Component } from '@angular/core';
import 'app/utility/extensions/stringExtension';

@Component({
  // tslint:disable-next-line
  selector: 'body',
  templateUrl: './app.component.html'
})
export class AppComponent { 
  constructor() {
    localStorage.setItem('isDevelopment', 'false');
  }
}
