
    import { Injectable } from '@angular/core';
    import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from 
    '@angular/router';
   import { Observable } from 'rxjs';
    import { AppAuthService } from './app-auth.service';
    import {Router} from '@angular/router';
    @Injectable()
    export class AppRouteGuard implements CanActivate {
      constructor(private auth: AppAuthService,
        private myRoute: Router){
      }
      canActivate(
        next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
        if(this.auth.isLoggedIn()){
          return true;
        }else{
          this.myRoute.navigate(["login"]);
          return false;
        }
      }
    }