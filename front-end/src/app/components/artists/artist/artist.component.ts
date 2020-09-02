import { Component, OnInit, Input, Output,EventEmitter } from '@angular/core';

@Component({
  selector: 'app-artist',
  templateUrl: './artist.component.html',
  styleUrls: ['./artist.component.css']
})
export class ArtistComponent implements OnInit {

  @Output() output=new EventEmitter();
  @Input() artist:any;
  constructor() { }

  ngOnInit(): void {
  }

 trigger()
 {
     this.output.emit(this.artist);
 }

}
