import { BrowserModule } from '@angular/platform-browser';
import { ModuleWithProviders, NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { AppComponent } from './app.component';
import { HomeComponent } from './modules/general/home/home.component';
import { ContactComponent } from './modules/general/contact/contact.component';
import { AboutComponent } from './modules/general/about/about.component';
import { SigninComponent } from './modules/general/signin/signin.component';
import { NotFoundComponent } from './modules/general/not-found/not-found.component';
import { AppRoutingModule } from './app-routing.module';
import {TranslateHttpLoader} from '@ngx-translate/http-loader';
import { AppAuthService } from './auth/app-auth.service';
import { AppRouteGuard } from './auth/auth-route-guard';
import { ShareConfig } from './shareConfig';
import {TabModule} from 'angular-tabs-component';
import {FormBuilder,Form,FormGroup,FormsModule} from'@angular/forms';
import {TranslateModule, TranslateLoader,TranslateService
} from '@ngx-translate/core';
import { ProfileComponent } from './profile/profile.component';
import { RentComponent } from './modules/rent/rent.component';
import { BuyandrentComponent } from './modules/buyandrent/buyandrent.component';
import { DetailebuyandrentComponent } from './modules/detailebuyandrent/detailebuyandrent.component';
import { DetailerentComponent } from './modules/detailerent/detailerent.component';
import { SubmitrentComponent } from './modules/submitrent/submitrent.component';
import { SubmitbuyandrentComponent } from './modules/submitbuyandrent/submitbuyandrent.component';
export function createTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ContactComponent,
    AboutComponent,
    SigninComponent,
    NotFoundComponent,
    ProfileComponent,
    RentComponent,
    BuyandrentComponent,
    DetailebuyandrentComponent,
    DetailerentComponent,
    SubmitrentComponent,
    SubmitbuyandrentComponent
    
  ],
  imports: [
    FormsModule,
    TabModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    HttpModule,
    TranslateModule.forRoot({
      loader: {
          provide: TranslateLoader,
          useFactory: (createTranslateLoader),
          deps: [HttpClient]
      }
  }),
  ],
  providers: [TranslateService,
  ShareConfig],
  bootstrap: [AppComponent]
})
export class AppModule {
  static forRoot(): ModuleWithProviders<AppModule> {
    return {
        ngModule: AppModule,
        providers: [
            AppAuthService,
            AppRouteGuard
        ]
    };
}
 }