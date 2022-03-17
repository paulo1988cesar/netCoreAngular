import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {
  isCollapsed = false;
  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  public showMenu(): boolean {
    return (this.router.url !== '/user/login' && this.router.url !== '/user/registration');
  }
}
