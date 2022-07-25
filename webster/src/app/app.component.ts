import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'webster';
  constructor(private router: Router, /*private authService: AuthService*/) {
    console.log("App component loaded");
    window.addEventListener('scroll', this.reveal);
  }

  reveal() {
    //Resolve this method.
    console.log("Scrolling");
    let reveals = document.querySelectorAll('.reveal');
    for (let step = 0; step < reveals.length; step++) {
      let windowHeight = window.innerHeight;
      let topItem = reveals[step].getBoundingClientRect().top;
      let elementVisible = 150;
      if (topItem < windowHeight - elementVisible) {
        reveals[step].classList.add('unfold');
        console.log(reveals[step] + "activated");
      } else {
        reveals[step].classList.remove('unfold');
      }
    }
  }

  onDevelopment() {
    this.router.navigate(['/development']);
  }
}
