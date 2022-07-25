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
    window.addEventListener('scroll', this.reveal);
    console.log("App component loaded");
  }

  reveal() {
    //Resolve this method.
    console.log("Scrolling");
    let reveals = document.getElementsByClassName("hidden");
    console.log(reveals);
    let windowHeight = window.innerHeight; //Get the window height.
    let elementVisible = 150; //The element is visible if it is 150px from the top of the screen.
    for (let step = 0; step < reveals.length; step++) {
      let topItem = reveals[step].getBoundingClientRect().top; //Get the top coordinate of the element.
      console.log("Window height: " + windowHeight);
      console.log("Top item: " + topItem);
      console.log("Element visible: " + elementVisible);
      if (topItem < windowHeight + elementVisible) {
        reveals[step].classList.remove("hidden");
        reveals[step].classList.add("reveal");
      }
      if (topItem < windowHeight - elementVisible - 1000) {
        reveals[step].classList.remove("hidden");
        reveals[step].classList.add('reveal');
        console.log(reveals[step] + " activated");
      }
    }
  }

  onDevelopment() {
    this.router.navigate(['/development']);
  }
}
