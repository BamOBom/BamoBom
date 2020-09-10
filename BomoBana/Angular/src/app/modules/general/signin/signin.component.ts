
import { Component, OnInit,Injectable ,EventEmitter, Output } from '@angular/core';

// import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AppAuthService } from '../../../auth/app-auth.service';
import { ShareServiceService } from '../../../share-service.service';
import { Observable } from 'rxjs';
@Component({
  selector: 'app-login',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent implements OnInit {
  form;
  @Output() getLoggedInName: EventEmitter<any> = new EventEmitter();
  username: Observable<string>;
  constructor(
    //private fb: FormBuilder,
    private myRoute: Router,
    private auth: AppAuthService,
    public shareservice:ShareServiceService) {
    // this.form = fb.group({
    //   email: ['', [Validators.required, Validators.email]],
    //   password: ['', Validators.required]
    // }
    // );
  }
  ngOnInit() {
  }

  // resetForm(form? : NgForm) {
  //   if(form !=null)
  //   form.reset();
  //   this.loginService.selectedLogin = {
  //     ID : null, 
  //     UserName : '', 
  //     Password : '',
  //     Auth : false
     
  //   }
  // }
  // login() {
  //   if (this.form.valid) {
  //     this.shareservice.login(this.form.value).subscribe(data => {
  //    //  var value = JSON.parse(data.);
  //     //  this.auth.sendToken(value)
  //       // this.shareservice.setusername(value.UserName);
  //        // this.resetForm(form);
  //         // this.router.navigate(['/']);
  //         this.myRoute.navigate(["home"]);
  //       });
     
  //   }
  // }
}
