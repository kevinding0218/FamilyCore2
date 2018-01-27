import { CalendarEvent } from 'calendar-utils';
import { CalendarEventTimesChangedEvent } from 'angular-calendar';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
    templateUrl: 'setup-entree-calendar.component.html'
  })
  export class SetupEntreeCalendarComponent implements OnInit {
    constructor() {

    }

    ngOnInit() {
    }

      // Calendar
  viewDate: Date = new Date();
  events: CalendarEvent[] = [];

  externalEvents: CalendarEvent[] = [
    {
      title: 'Event 1',
      color: {primary: 'red', secondary:'blue'},
      start: new Date(),
      draggable: true
    },
    {
      title: 'Event 2',
      color: {primary: 'red', secondary:'blue'},
      start: new Date(),
      draggable: true
    }
  ];

  eventDropped({
    event,
    newStart,
    newEnd
  }: CalendarEventTimesChangedEvent): void {
    const externalIndex: number = this.externalEvents.indexOf(event);
    if (externalIndex > -1) {
      this.externalEvents.splice(externalIndex, 1);
      this.events.push(event);
    }
    event.start = newStart;
    if (newEnd) {
      event.end = newEnd;
    }
    this.viewDate = newStart;
  }
  }