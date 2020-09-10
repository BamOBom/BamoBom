    import * as moment from "moment";
    import { Injectable } from '@angular/core';
    import { Router } from '@angular/router';
    @Injectable()
    export class AppAuthService {
      constructor(private myRoute: Router) { }
      sendToken(token: string) {
        localStorage.setItem("LoggedInUser", token)
      }
      getToken() {
        return localStorage.getItem("LoggedInUser")
      }
      isLoggedIn() {
        return this.getToken() !== null;
      }
      logout() {
        localStorage.removeItem("LoggedInUser");
        this.myRoute.navigate(["signin"]);
      }
      private setSession(authResult) {
        const expiresAt = moment().add(authResult.expiresIn,'second');

        localStorage.setItem('id_token', authResult.idToken);
        localStorage.setItem("expires_at", JSON.stringify(expiresAt.valueOf()) );
    }   
    // logout() {
    //     localStorage.removeItem("id_token");
    //     localStorage.removeItem("expires_at");
    // }

    // public isLoggedIn() {
    //     return moment().isBefore(this.getExpiration());
    // }

    isLoggedOut() {
        return !this.isLoggedIn();
    }

    getExpiration() {
        const expiration = localStorage.getItem("expires_at");
        const expiresAt = JSON.parse(expiration);
        return moment(expiresAt);
    }  
    }