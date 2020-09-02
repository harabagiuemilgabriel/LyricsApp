import {
    trigger,
    state,
    style,
    animate,
    transition,
    // ...
  } from '@angular/animations';

export var fade= trigger('fade',[
    state('void',style({opacity:0})),
    transition(':enter, :leave',[
        animate(150)
    ])
]);
