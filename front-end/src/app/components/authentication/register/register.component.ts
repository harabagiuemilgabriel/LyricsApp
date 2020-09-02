import { Component, OnInit } from '@angular/core';
import { LoginService } from '../../../services/login/login.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private loginService:LoginService) { }

  disabled:boolean=false;

  formModel={
    Email:"",
    Password:"",
    ConfirmPassword:"",
    ConfirmEmail:0
  };

  confirmPasswordErrorMessege:string=null;

  ngOnInit(): void {
  }

  CheckPassword()
  {
    if(this.formModel.Password!=this.formModel.ConfirmPassword)
    {
      this.confirmPasswordErrorMessege="Your passowd and confirm passowd dont match";
      this.disabled=false;
    }
    else
    {
      this.disabled=true;
      this.confirmPasswordErrorMessege=null;
    }
  }

  onSubmit()
  {
      this.loginService.Register(this.formModel).subscribe(
        succ=>{
          console.log("Register Success", succ);
        },
        err=>console.log("Register Failed",err)
      )
  }

}
