import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, BehaviorSubject, empty, throwError } from 'rxjs';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { tap, switchMap, filter, take, catchError } from 'rxjs/operators';
import { LoginService } from '../services/login/login.service';
import * as jwt_decode from "jwt-decode";
import { ThrowStmt } from '@angular/compiler';

@Injectable()

export class AuthInterceptor implements HttpInterceptor{

    private isRefreshing=false;

    constructor(private router: Router,private loginService:LoginService)
    {

    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if(localStorage.getItem("JWT_TOKEN")!=null)
        {
                req=this.addAuthHeader(req);
                //console.log(req);
                return next.handle(req).pipe(
                    catchError((error:HttpErrorResponse)=>{
                        //console.log("error is",error,req);
                        if(error.status===401 && !this.isRefreshing)
                        {
                                //console.log("Error 401");
                                return this.refreshAccessToken().pipe(
                                    switchMap(()=>{
                                    //  console.log("SwitchMap Executed");
                                        req=this.addAuthHeader(req);
                                        return next.handle(req);
                                    }),
                                    catchError((err:any)=>{
                                    // console.log("Error catched",err,req);
                                        return empty();
                                    })
                                )
                        
                        }
                    return throwError(error);
                    })
                )
            
        }
        else
        {
            //console.log("clone return");
            return next.handle(req.clone());
        }
    }


    private refreshAccessToken()
    {
        this.isRefreshing=true;
        //console.log("RefreshAccessToken function");
        return this.loginService.refreshToken().pipe(
            tap(()=>{
                this.isRefreshing=false;
               // console.log("AccessToken Refreshed!");
            })
        );
    }

    private addAuthHeader(req:HttpRequest<any>)
    {
        let ret=req.clone({headers: req.headers.set('Authorization', 'Bearer '+localStorage.getItem("JWT_TOKEN"))});
      //  console.log("ret",ret);
        return ret;
    }
}