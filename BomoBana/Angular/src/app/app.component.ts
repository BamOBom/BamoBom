import { Component } from '@angular/core';
import {Location} from '@angular/common';
import {TranslateService
} from '@ngx-translate/core';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  home: boolean = true;
  rezervitem: boolean = false;
  constructor(location: Location,public translate: TranslateService
   
) {
  translate.setDefaultLang('fa');
  translate.use('fa');
}
rezerv(){
  debugger
  this.rezervitem=true;
  this.home=false;
}
}
