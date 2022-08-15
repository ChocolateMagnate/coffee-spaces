import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CodeComponent } from './code/code.component';
import { CharsComponent } from './chars/chars.component';
import { ProfileComponent } from './profile/profile.component';
import { WelcomeComponent } from './welcome/welcome.component';
import { SubmitBugComponent } from './submit-bug/submit-bug.component';
import { DevelopmentComponent } from './development/development.component';

const routes: Routes = [
      {path: 'home', component: HomeComponent},
      {path: 'code', component: CodeComponent},
      {path: 'fetch', component: CharsComponent},
      {path: 'welcome', component: WelcomeComponent},
      {path: 'profile', component: ProfileComponent},
      {path: 'submit-bug', component: SubmitBugComponent},
      {path: 'development', component: DevelopmentComponent}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
