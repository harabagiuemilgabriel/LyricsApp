import { Component, OnInit, Input } from '@angular/core';
import { LyricProcessorService } from '../../../services/lyric-processor.service';

@Component({
  selector: 'app-lyric',
  templateUrl: './lyric.component.html',
  styleUrls: ['./lyric.component.css']
})

export class LyricComponent implements OnInit {

@Input() lyrics:any;
data=[];

  constructor(private service:LyricProcessorService) { }

  ngOnInit(): void {
    console.log(this.lyrics)
    this.data=this.lyrics;
  }

}
