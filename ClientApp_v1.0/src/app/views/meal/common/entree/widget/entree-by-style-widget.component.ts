import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'entree-by-style-widget',
    templateUrl: './entree-by-style-widget.component.html'
})


export class EntreeByStyleWidgetComponent implements OnInit {
    @Input() styleCount: number;
    @Input() styleId: number;
    @Input() styleName: string;
    @Input() styleValue: number;
    @Input() styleBgId: number;
    stylePercent: string;

    constructor() { }

    ngOnInit() { 
        this.stylePercent = this.styleValue.toString() + '%';
    }
}