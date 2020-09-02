import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LyricProcessorService {

  constructor() { }


  Process(data:any):string[]
  {
    let Lyrics=[];
    let lyric='';
    let index=0;
    let cuv=0;
    for(let i=0;i<data.lyrics.length;i++)
    {
      if(data.lyrics[i]=="<")
      {
        i=i+3;
        index++;
        Lyrics[index]=lyric;
        lyric='';
      }
      else
      {
        lyric+=data.lyrics[i]
      }
    }
    return Lyrics;
  }
}
