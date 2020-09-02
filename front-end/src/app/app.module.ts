import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ReactiveFormsModule,FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavigationComponent } from './components/navigation/navigation.component';
import { ArtistsComponent } from './components/artists/artists.component';
import { ArtistComponent } from './components/artists/artist/artist.component';
import { LyricsComponent } from './components/artists/lyrics//lyrics.component';
import { ProfileComponent } from './components/profile/profile.component';
import { SongsComponent } from './components/artists/songs/songs.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/authentication/login/login.component';
import { RegisterComponent } from './components/authentication/register/register.component';
import { SongComponent } from './components/home/song/song.component';
import { LyricComponent } from './components/home/lyric/lyric.component';
import { HomeArtistComponent } from './components/home/home-artist/home-artist.component';
import { PopUpArtistComponent } from './components/artists/pop-up-artist/pop-up-artist.component';
import { AuthInterceptor } from './auth/auth.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    NavigationComponent,
    ArtistsComponent,
    LyricsComponent,
    ProfileComponent,
    ArtistComponent,
    SongsComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    SongComponent,
    LyricComponent,
    HomeArtistComponent,
    PopUpArtistComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    BrowserAnimationsModule
  ],
  providers: [{
    provide:HTTP_INTERCEPTORS,
    useClass:AuthInterceptor,
    multi:true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
