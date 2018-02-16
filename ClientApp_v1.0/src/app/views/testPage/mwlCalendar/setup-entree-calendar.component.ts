import {
  Component,
  OnInit,
  ChangeDetectionStrategy,
  ViewChild,
  TemplateRef
} from '@angular/core';
import {
  startOfDay,
  endOfDay,
  subDays,
  addDays,
  endOfMonth,
  isSameDay,
  isSameMonth,
  addHours
} from 'date-fns';
import { Subject } from 'rxjs/Subject';
import {
  CalendarEvent,
  CalendarEventAction,
  CalendarEventTimesChangedEvent
} from 'angular-calendar';

const colors: any = {
  red: {
    primary: '#ad2121',
    secondary: '#FAE3E3'
  },
  blue: {
    primary: '#1e90ff',
    secondary: '#D1E8FF'
  },
  yellow: {
    primary: '#e3bc08',
    secondary: '#FDF1BA'
  },
  orange: {
    primary: '#ea6804',
    secondary: '#e5ac80'
  }
};

@Component({
  templateUrl: 'setup-entree-calendar.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SetupEntreeCalendarComponent implements OnInit {
  view: string = 'week';

  viewDate: Date = new Date();

  actions: CalendarEventAction[] = [
    {
      label: '<i class="fa fa-fw fa-pencil"></i>',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.handleEvent('Edited', event);
      }
    },
    {
      label: '<i class="fa fa-fw fa-times"></i>',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.initialEvents = this.initialEvents.filter(iEvent => iEvent !== event);
        this.handleEvent('Deleted', event);
      }
    }
  ];

  refresh: Subject<any> = new Subject();

  initialEvents: CalendarEvent[] = [
    // {
    //   start: subDays(new Date(), 1),
    //   end: addDays(new Date(), 1),
    //   title: 'A 3 day event',
    //   color: colors.red,
    //   actions: this.actions,
    //   draggable: true
    // },
    // {
    //   start: startOfDay(new Date()),
    //   title: 'An event with no end date',
    //   color: colors.yellow,
    //   actions: this.actions,
    //   draggable: true
    // },
    // {
    //   start: new Date(),
    //   end: addDays(new Date(), 1),
    //   title: 'A 2 day event',
    //   color: colors.blue,
    //   draggable: true
    // }
  ];

  externalEvents: CalendarEvent[] = [
    {
      title: 'Entree 1',
      color: colors.yellow,
      start: new Date(),
      end: addDays(new Date(), 1),
      allDay: true,
      draggable: true
    },
    {
      title: 'Entree 2',
      color: colors.blue,
      start: new Date(),
      end: addDays(new Date(), 1),
      allDay: true,
      draggable: true
    }
  ];

  activeDayIsOpen: boolean = true;

  constructor() { }

  ngOnInit() {
    this.initialEvents = [
      {
        start: subDays(startOfDay(new Date()), 1),
        //end: new Date(),
        title: 'Entree yesterday',
        color: colors.red,
        actions: this.actions,
        allDay: true,
        draggable: true
      },
      {
        start: startOfDay(new Date()),
        //end: new Date(),
        title: 'Entree today',
        color: colors.yellow,
        actions: this.actions,
        allDay: true,
        draggable: true
      },
      {
        start: addDays(startOfDay(new Date()), 1),
        //end: addDays(startOfDay(new Date()), 2),
        title: 'Entree tomorrow',
        color: colors.orange,
        actions: this.actions,
        resizable: {
          beforeStart: true,
          afterEnd: true
        },
        draggable: true
      }, {
        start: addDays(startOfDay(new Date()), 2),
        //end: addDays(startOfDay(new Date()), 3),
        title: 'Entree tomorrow after tomorrow',
        color: colors.blue,
        actions: this.actions,
        resizable: {
          beforeStart: true,
          afterEnd: true
        },
        draggable: true
      }
    ];
    console.log('ngOnInit initialEvents', this.initialEvents);
  }

  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    console.log('dayClicked date', date);
    console.log('dayClicked events', events);
    if (isSameMonth(date, this.viewDate)) {
      if (
        (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
        events.length === 0
      ) {
        this.activeDayIsOpen = false;
      } else {
        this.activeDayIsOpen = true;
        this.viewDate = date;
      }
    }
  }

  eventTimesChanged({
    event,
    newStart,
    newEnd
  }: CalendarEventTimesChangedEvent): void {
    console.log('eventTimesChanged newStart', newStart);
    console.log('eventTimesChanged newEnd', newEnd);
    console.log('eventTimesChanged event', event);
    event.start = newStart;
    event.end = newEnd;
    this.handleEvent('Dropped or resized', event);
    this.refresh.next();
    console.log('eventTimesChanged refresh', this.refresh);
    console.log('eventTimesChanged initialEvents', this.initialEvents);
  }

  handleEvent(action: string, event: CalendarEvent): void {
    console.log('action is ' + action + '\nCalendarEvent is: ', event);
  }

  changeEventLog: string[] = [];

  eventDropped({
    event,
    newStart,
    newEnd
  }: CalendarEventTimesChangedEvent): void {
    console.log('eventDropped newStart', newStart);
    console.log('eventDropped newEnd', newEnd);
    console.log('eventDropped event', event);
    this.changeEventLog.push('Old Event - ' + event.title + ' between ' +
    event.start.toLocaleDateString() + ' changed to ' + newStart.toLocaleDateString());
    const externalIndex: number = this.externalEvents.indexOf(event);
    if (externalIndex > -1) {
      this.externalEvents.splice(externalIndex, 1);
      this.initialEvents.push(event);
    }
    event.start = newStart;
    if (newEnd) {
      event.end = newEnd;
    }
    this.viewDate = newStart;
    this.activeDayIsOpen = true;
    console.log('eventDropped initialEvents', this.initialEvents);
  }

  addEvent(): void {
    this.initialEvents.push({
      title: 'New event',
      start: startOfDay(new Date()),
      end: endOfDay(new Date()),
      color: colors.red,
      draggable: true,
      resizable: {
        beforeStart: true,
        afterEnd: true
      }
    });
    this.refresh.next();
    console.log('eventTimesChanged refresh', this.refresh);
  }

  //Date Range Picker
  public daterange: any = {};
  // see original project for full list of options
  // can also be setup using the config service to apply to multiple pickers
  public options: any = {
    locale: { format: 'YYYY-MM-DD' },
    alwaysShowCalendars: false,
  };

  public selectedDate(value: any, datepicker?: any) {
    // this is the date the iser selected
    console.log(value);

    // any object can be passed to the selected event and it will be passed back here
    datepicker.start = value.start;
    datepicker.end = value.end;

    // or manupulat your own internal property
    this.daterange.start = value.start;
    this.daterange.end = value.end;
    this.daterange.label = value.label;
  }
}




