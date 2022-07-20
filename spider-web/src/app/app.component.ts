import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Input } from '@angular/core';
//import { AuthService } from './auth.service';
/*TO-DO:
1. Resolve AuthService import;
2. Implement login-system;
3. Implement registeration;
4. Implement bug reports and SQL.*/

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  /*Properties have no initializer and is not definitely assigned in the constructor:
  @Input() isLoggedIn: boolean;
  @Input() username: string;
  @Input() position: string;
  @Input() password: string;*/

  onLogin(): void {

  }
}
