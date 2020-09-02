import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserModel } from '../Models/user-model';
import { tap, catchError } from 'rxjs/operators';
 
@Injectable({
  providedIn: 'root'
})
export class MainService {

  count:number=0;

  artistsUrl:string="https://localhost:44328/artists";
  songUrl:string="https://localhost:44328/songs";
  lyricsUrl:string="https://localhost:44328/lyrics";

  constructor(private http:HttpClient) {}

  getArtists():Observable<any>
  {
    return this.http.get(this.artistsUrl);
  }

  getSongsForArtist(id:number):Observable<any>
  {
    return this.http.get(this.songUrl+"/"+id);
  }

  getLyrics(id:number):Observable<any>
  {
    return this.http.get(this.lyricsUrl+"/"+id);
  }
  getArtistBySong(id:number):Observable<any>
  {
    return this.http.get(this.artistsUrl+"/id/"+id);
  }

}
