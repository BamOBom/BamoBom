import { Component, OnInit } from '@angular/core';
import { NgForm,FormControl } from '@angular/forms';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  favoriteColorControl = new FormControl('');
  isValidFormSubmitted = false;
	allProfiles: any;
	allTechnologies: any;
  userForm: any;
  selectedMonth = null;
  constructor() { }

  ngOnInit(): void {

	

  }


}
