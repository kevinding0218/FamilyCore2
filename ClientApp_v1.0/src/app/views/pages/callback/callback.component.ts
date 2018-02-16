import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-callback',
  templateUrl: './callback.component.html',
  styleUrls: ['./callback.component.css']
})
export class CallbackComponent implements OnInit {

  constructor(private _route: ActivatedRoute,
    private _router: Router,) { 
      console.log('ActivatedRoute: ', this._route);
      console.log('Router: ', this._router);
    }

  ngOnInit() {
  }

}
