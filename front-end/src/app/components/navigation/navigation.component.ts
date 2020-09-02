import { Component, OnInit, Injectable } from '@angular/core';
import { LoginService } from 'src/app/services/login/login.service';


@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  constructor(private loginService:LoginService) { }
  
  LoggedIn:boolean=false;

  ngOnInit(): void {
    setInterval(()=>{!!localStorage.getItem('JWT_TOKEN')?this.LoggedIn=true:this.LoggedIn=false})
  }

  logOut()
  {
    this.loginService.LogOut().subscribe(res=>{
      this.LoggedIn=!res});
  }

}
