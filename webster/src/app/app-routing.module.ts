import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
import { DevelopmentComponent } from './development/development.component';

const routes: Routes = [{path: 'home', component: HomeComponent},
      {path: 'profile', component: ProfileComponent},
      {path: 'development', component: DevelopmentComponent}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
