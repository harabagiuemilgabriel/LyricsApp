import { Component, OnInit,Input } from '@angular/core';
import { MainService } from '../../../services/main.service';
import { LyricProcessorService } from '../../../services/lyric-processor.service';
 
@Component({
  selector: 'app-songs',
  templateUrl: './songs.component.html',
  styleUrls: ['./songs.component.css']
})
export class SongsComponent implements OnInit {

  @Input() song:any;
  @Input() focusSong:number;
  open:boolean=false;
  lyrics:any;

  constructor(private service:MainService,private lyricsProcessor:LyricProcessorService) { }

  ngOnInit(): void {
    if(this.focusSong!=-1)
    {
      if(this.song.songId==this.focusSong)
      {
        this.seeLyrics();
      }
    }
  }

  seeLyrics()
  {
    this.service.getLyrics(this.song.songId).subscribe(data=>{
      if(data!=null)
        this.lyrics=this.lyricsProcessor.Process(data);
      else this.lyrics=data;
    });
    this.open=!this.open;
  }

  closeLyrics()
  {
    this.open=!this.open;
    this.focusSong=null;
    this.lyrics=null;
  }

}
