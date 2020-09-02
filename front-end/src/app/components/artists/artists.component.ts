import { Component, OnInit } from '@angular/core';
import { MainService } from '../../services/main.service';
import { fade} from '../Animations/main-animations';


@Component({
  selector: 'app-artists',
  templateUrl: './artists.component.html',
  styleUrls: ['./artists.component.css'],
  animations:[fade]
})
export class ArtistsComponent implements OnInit {

  artists:any;
  focusArtist:any;
  isOpen:boolean=false;

  constructor(private service:MainService) { }

  ngOnInit(): void {
    this.service.getArtists().subscribe(data=>
      {
        this.artists=data;
        console.log(this.artists);
      }
      );
  }

  changeIsOpen()
  {
    this.isOpen=!this.isOpen;
  }

  openWindow(data)
  {
      this.focusArtist=data;
      this.changeIsOpen();
  }

  closeWindow()
  {
    this.focusArtist=null;
    this.changeIsOpen();
  }

}
