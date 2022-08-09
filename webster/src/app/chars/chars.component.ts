import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
//This component remains as a simple way to verify if the connection to the server is etablished.
@Component({
  selector: 'app-chars',
  templateUrl: './chars.component.html',
  styleUrls: ['./chars.component.css']
})
export class CharsComponent implements OnInit {

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    let headers = new HttpHeaders();
    headers.append('Access-Control-Allow-Origin', '*');
    this.http.get("http://localhost:5226/fetch", {headers}).subscribe(
      response => {console.log(response)}, error => {console.log(error)});
  }
}