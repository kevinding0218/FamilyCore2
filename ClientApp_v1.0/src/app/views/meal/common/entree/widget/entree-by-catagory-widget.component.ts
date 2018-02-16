import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'entree-by-catagory-widget',
    templateUrl: './entree-by-catagory-widget.component.html'
})


export class EntreeByCatagoryWidgetComponent implements OnInit {
    @Input() catagoryCount: number;
    @Input() catagoryId: number;
    @Input() catagoryName: string;
    @Input() catagoryValue: number;
    @Input() styleBgId: number;
    catagoryPercent: string;

    constructor() { }

    ngOnInit() { 
        this.catagoryPercent = this.catagoryValue.toString() + '%';
    }
}