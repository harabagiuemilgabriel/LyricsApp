import { Component, OnInit } from '@angular/core';
import { SearchService } from '../../services/search.service';



@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  inputSearch:string='';
  inputSearchLength:number=0;

  focusSong:Object;
  focusArtist:Object;

  open:boolean=false;

  artists:Object[];
  songs:Object[];
  lyrics:Object[];


  constructor(private service:SearchService) { }

  ngOnInit(): void {
  }

  InputChanged():void
  {
      if(this.inputSearch!='')
      {
        this.service.SearchArtist(this.inputSearch).subscribe(data=>
                                                          { 
                                                            this.artists=data;
                                                          });
        this.service.SearchSongs(this.inputSearch).subscribe(data=>
                                                          { 
                                                            this.songs=data;
                                                          }); 
        this.service.SearchLyrics(this.inputSearch).subscribe(data=>
                                                          { 
                                                            this.lyrics=data;
                                                          });                                                  
      }
      else
      {
          this.artists=[];
          this.songs=[];
          this.lyrics=[];
      }
  }

  FocusSong(data:Object)
  {
    if(!this.open)
    {
      this.focusSong=data;
      this.open=!this.open;
    }
  }

  FocusArtist(data:Object)
  {
    if(!this.open)
    {
      this.focusArtist=data;
      this.open=!this.open;
    }
  }
  
  closePopUp()
  {
    this.focusArtist=null;
    this.focusSong=null;
    this.open=!this.open;
  }

}
