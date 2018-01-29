import { Directive, HostListener, ElementRef } from '@angular/core';

@Directive({
  selector: '[appNavDropdown]'
})
export class NavDropdownDirective {

  constructor(private el: ElementRef) { }

  toggle() {
    // const hostElem = this.el.nativeElement;
    // console.log('this.el.nativeElement: ', hostElem);
    // console.log('this.el.nativeElement.children: ', hostElem.children);
    // console.log('this.el.nativeElement.parentNode: ', hostElem.parentNode);
    this.el.nativeElement.classList.toggle('open');
  }
}

/**
* Allows the dropdown to be toggled via click.
*/
@Directive({
  selector: '[appNavDropdownToggle]'
})
export class NavDropdownToggleDirective {
  constructor(private dropdown: NavDropdownDirective) { }

  @HostListener('mouseover', ['$event'])
  toggleOpen($event: any) {
    $event.preventDefault();
    this.dropdown.toggle();
  }
}

/**
* Allows the dropdown to be toggled via click.
*/
@Directive({
  selector: '[appNavDropdownItemClickToggle]'
})
export class NavDropdownItemClickToggleDirective {
  constructor(private el: ElementRef) { }

  @HostListener('click', ['$event'])
  toggleParentNodeCloseOnClick($event: any) {
    $event.preventDefault();
    // const hostElem = this.el.nativeElement;
    // console.log('this.el.nativeElement: ', hostElem);
    // console.log('this.el.nativeElement.children: ', hostElem.children);
    // console.log('this.el.nativeElement.parentNode: ', hostElem.parentNode);
    this.el.nativeElement.parentNode.parentNode.classList.toggle('open');
  }
}

export const NAV_DROPDOWN_DIRECTIVES = [NavDropdownDirective, NavDropdownToggleDirective, NavDropdownItemClickToggleDirective];
