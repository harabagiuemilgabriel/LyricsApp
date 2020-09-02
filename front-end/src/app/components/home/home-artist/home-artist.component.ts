import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-home-artist',
  templateUrl: './home-artist.component.html',
  styleUrls: ['./home-artist.component.css']
})
export class HomeArtistComponent implements OnInit {

@Input() artist:any;

  constructor() { }

  ngOnInit(): void {
  }

}
