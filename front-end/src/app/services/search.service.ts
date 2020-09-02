import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';



@Injectable({
  providedIn: 'root'
})
export class SearchService {

  artistsUrl:string="https://localhost:44328/artists";
  songsUrl:string="https://localhost:44328/songs";
  lyricsUrl:string="https://localhost:44328/lyrics";


  constructor(private http:HttpClient) { }

  SearchArtist(data:string):Observable<any>
  {
    return this.http.get(this.artistsUrl+"/search/"+data);
  }
  SearchSongs(data:string):Observable<any>
  {
    return this.http.get(this.songsUrl+"/search/"+data);
  }
  SearchLyrics(data:string):Observable<any>
  {
    return this.http.get(this.lyricsUrl+"/search/"+data);
  }
}
