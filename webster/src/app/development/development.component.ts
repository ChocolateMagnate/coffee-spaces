import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-development',
  templateUrl: './development.component.html',
  styleUrls: ['./development.component.css']
})
export class DevelopmentComponent implements OnInit {

  constructor() {
    console.log("Development component loaded");
   }

  ngOnInit(): void {
  }

}
