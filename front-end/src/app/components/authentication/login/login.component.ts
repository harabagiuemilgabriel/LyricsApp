import { Component, OnInit } from '@angular/core';
import { LoginService } from '../../../services/login/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  formModel={
    Email:"",
    Password:""
  };

  constructor( private loginService:LoginService, private router:Router) { }

  ngOnInit(): void {
  }

  onSubmit()
  {
    this.loginService.Login(this.formModel).subscribe();
  }

  toRegister()
  {
    console.log("to register");
    this.router.navigate(['register']);
  }
}
