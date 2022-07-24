import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
//import { AuthService } from './auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'webster';
  constructor(private router: Router, /*private authService: AuthService*/) {
    console.log("App component loaded");
  }
  onDevelopment() {
    this.router.navigate(['/development']);
  }
}
