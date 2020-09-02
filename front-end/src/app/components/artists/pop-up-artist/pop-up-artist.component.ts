import { Component, OnInit,Input,Output,EventEmitter } from '@angular/core';
import { MainService } from '../../../services/main.service';

@Component({
  selector: 'app-pop-up-artist',
  templateUrl: './pop-up-artist.component.html',
  styleUrls: ['./pop-up-artist.component.css'],
})
export class PopUpArtistComponent implements OnInit {

@Input() focusArtist:any;
@Input() focusSong:any;
@Output() closeWindow=new EventEmitter();


songs:any;

  constructor(private service:MainService) { }

  ngOnInit(): void {
      if(this.focusArtist)
      {
        this.focusSong={focusId:-1};
        console.log(this.focusArtist);
        this.getSongs();
      }
      else if(this.focusSong){
      this.service.getArtistBySong(this.focusSong.author).subscribe(data=>{
            this.focusArtist=data;
            this.getSongs();
          });
      }
  }

  getSongs()
  {
    this.service.getSongsForArtist(this.focusArtist.artistId).subscribe(data=>{this.songs=data});
  }

  close()
  {
    console.log("close");
    this.closeWindow.emit(true);
  }

}
