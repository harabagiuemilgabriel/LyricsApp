import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap, mapTo } from 'rxjs/operators';
import { ThrowStmt } from '@angular/compiler';
import { Route } from '@angular/compiler/src/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private baseUrl="https://localhost:44328/UsersTables";

  private user:any;

  constructor( private http:HttpClient,private router:Router) { }

  Login(UserCredentials): Observable<Object>
  {
   return this.http.post(this.baseUrl+"/Login",UserCredentials).pipe(
     tap(
       succ=>{
         this.user=succ;
         this.AddJwtTokens(succ);
       }
     )
   );
  }

  LogOut():Observable<any>
  {
    console.log("This is the user",this.user);
    return this.http.post(this.baseUrl+"/logOut",this.user).pipe(
      tap(
        succ=>{
          this.user=null;
          localStorage.removeItem('JWT_TOKEN');
          localStorage.removeItem('RF_JWT_TOKEN');
          this.router.navigate(['']);
        },
        err=>console.log(err)
      )
    );
  }

  Register(UserCredentials):Observable<any>
  {
     return this.http.post(this.baseUrl+'/register',UserCredentials).pipe(
        tap(
          succ=>{
            this.AddJwtTokens(succ);
          }
        )
      )
  }

  refreshToken()
  {
    let token=localStorage.getItem("JWT_TOKEN");
    let rfToken=localStorage.getItem("RF_JWT_TOKEN");
    return this.http.post(this.baseUrl+"/refresh",{token:token,refreshToken:rfToken}).pipe(
      tap(
        (succ:any)=>{
          this.storeToken(succ.token);
        }
      )
    );
  }

  storeToken(token:string):boolean
  {
    //console.log("Token stored, token:",token);
    if(token!=null)
    {
      localStorage.setItem('JWT_TOKEN',token);
      return true;
    }
    return false;
  }

  AddJwtTokens(res:any)
  {
    localStorage.setItem('JWT_TOKEN',res.token);
    localStorage.setItem('RF_JWT_TOKEN',res.refreshToken);
    this.router.navigateByUrl('');
  }

}
