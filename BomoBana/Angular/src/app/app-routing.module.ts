import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppRouteGuard } from './auth/auth-route-guard';
import { AppAuthService } from './auth/app-auth.service';
import { AboutComponent } from './modules/general/about/about.component';
import { ContactComponent } from './modules/general/contact/contact.component';
import { HomeComponent } from './modules/general/home/home.component';
import { SigninComponent } from './modules/general/signin/signin.component';
import { NotFoundComponent } from './modules/general/not-found/not-found.component';
import { ProfileComponent } from './modules/profile/profile.component';
import { BuyandrentComponent } from './modules/buyandrent/buyandrent.component';
import { RentComponent } from './modules/rent/rent.component';
import { DetailebuyandrentComponent } from './modules/detailebuyandrent/detailebuyandrent.component';
import { DetailerentComponent } from './modules/detailerent/detailerent.component';
import { SubmitbuyandrentComponent } from './modules/submitbuyandrent/submitbuyandrent.component';
import { SubmitrentComponent } from './modules/submitrent/submitrent.component';
const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'contact', component: ContactComponent, canActivate: [AppRouteGuard] },
  { path: 'about', component: AboutComponent, canActivate: [AppRouteGuard] },
  { path: 'signin', component: SigninComponent},
  { path: 'profile', component: ProfileComponent},
  { path: 'buyandrent', component: BuyandrentComponent},
  { path: 'detailebuyandrent', component: DetailebuyandrentComponent},
  { path: 'rent', component: RentComponent},
  { path: 'detailerent', component: DetailerentComponent},
  { path: 'submitrent', component: SubmitrentComponent},
  { path: 'sbmitbuyandrent', component: SubmitbuyandrentComponent},
  { path: '**', component: NotFoundComponent}
];

@NgModule({
  
  imports: [RouterModule.forRoot(routes, {useHash: true})],
  exports: [RouterModule],
  providers: [AppAuthService,AppRouteGuard],
  declarations: []
})
export class AppRoutingModule { }
