import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-transport-card',
  templateUrl: './transport-card.component.html',
  styleUrls: ['./transport-card.component.scss']
})
export class TransportCardComponent implements OnInit {
  constructor(
    router: Router
    ) { 
    router.navigate(['transport-card/section-a']);
    }

  ngOnInit(): void {
  
  }
}
