import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
import { DevelopmentComponent } from './development/development.component';
import { LostComponent } from './lost/lost.component';
import { CharsComponent } from './chars/chars.component';
import { SubmitBugComponent } from './submit-bug/submit-bug.component';
import { CodeComponent } from './code/code.component';
import { WelcomeComponent } from './welcome/welcome.component';

@NgModule({
  declarations: [
    AppComponent, HomeComponent, ProfileComponent, DevelopmentComponent,
    LostComponent, CharsComponent, SubmitBugComponent, CodeComponent, WelcomeComponent
  ],
  imports: [
    BrowserModule, AppRoutingModule, HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { 
  constructor(private http: HttpClient) {
    console.log("App module loaded");
  }
}
