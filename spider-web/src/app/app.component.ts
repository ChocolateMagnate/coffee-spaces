import { Component, Input } from '@angular/core';
import { HomeComponent } from './home/home.component';
import { Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpHandler } from '@angular/common/http';
/*TO-DO:
1. Resolve AuthService import;
2. Implement login-system;
3. Implement registeration;
4. Implement bug reports and SQL.*/

const routes: Routes = [{ path: 'home', component: HomeComponent},
  {path: '', redirectTo: '/spider-web/src/app/home/home.component.ts', pathMatch: 'full'}];
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  //Properties have no initializer and is not definitely assigned in the constructor:
  title = 'spider-web';
  @Input() username: string = "";
  @Input() password: string = "";
  @Input() position: string = "";
  constructor(private http: HttpClient) {
    console.log("AppComponent loaded: ");

  }

  onLogin() {
    console.log("Login button clicked.");
    console.log("Username: " + this.username);
    console.log("Password: " + this.password);
    console.log("Position: " + this.position);
  }
}
