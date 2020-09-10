import { Component, OnInit,Injectable ,EventEmitter, Output } from '@angular/core';
import { ShareConfig } from '../app/shareConfig';
import { Http,Response,Headers,RequestOptions,RequestMethod } from '@angular/http';
import { Observable,BehaviorSubject } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class ShareServiceService {
  @Output() getLoggedInName: EventEmitter<any> = new EventEmitter();
  username: Observable<string>;
  private _authNavStatusSource = new BehaviorSubject<boolean>(false);
  // Observable navItem stream
  authNavStatus$ = this._authNavStatusSource.asObservable();
  headerOptions = new Headers({'Content-Type': 'application/json',
    'Access-Control-Allow-Origin': '*',
    'Access-Control-Allow-Credentials': 'true',
    'Access-Control-Allow-Headers': 'Content-Type',
    'Access-Control-Allow-Methods': 'GET,PUT,POST,DELETE',
    'key': 'x-api-key',
    'value': 'NNctr6Tjrw9794gFXf3fi6zWBZ78j6Gv3UCb3y0x',});
  constructor(public config:ShareConfig,private http : Http) { }
  setusername(username:Observable<string>){  
    this.getLoggedInName.emit(username);    
    return true;
  }
  login(valu:any ) {
      var headerOptions = new Headers({'content-Type':'application/json','Access-Control-Allow-Origin': '*'});
    var requestOptions = new RequestOptions({method : RequestMethod.Post,headers : headerOptions});
    return this.http.post(ShareConfig+'/api/login',valu,requestOptions);
}
}
