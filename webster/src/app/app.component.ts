import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

interface Response {
  letters: string[];
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor(private http: HttpClient, private router: Router /*, private authService: AuthService*/) {
    console.log("App component loaded");
  }
  ngOnInit() {
    if (window.location.href === "http://localhost:4200/") {
      this.router.navigate(["/welcome"]);
  }
}

  onLogin() {
    this.http.get<Response>("http://localhost:7093/fetch").subscribe(
      response => {console.log(response)}, error => {console.log(error)});
  }


  onDevelopment() {
    this.router.navigate(['/development']);
  }

  onHome() {
    this.router.navigate(['/home']);
  }

  onSubmitBug() {
    this.router.navigate(['/submit-bug']);
  }

  onCode() {
    window.location.href = "http://localhost:4200/code";
  }

  onProfile() {
    this.router.navigate(['/profile']);
  }
}

