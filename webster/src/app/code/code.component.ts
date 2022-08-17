import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-code',
  templateUrl: './code.component.html',
  styleUrls: ['./code.component.css']
})
export class CodeComponent implements OnInit {
  dataset = ["summit", "Octopus", "Genesis"]
  constructor() { }

  ngOnInit(): void {
    let items = document.getElementsByClassName("repository").namedItem("repository");  
    console.log(items);
  }

  check(item: HTMLElement) {
    console.log(item);
    item.classList.toggle("checked");
  }

}
