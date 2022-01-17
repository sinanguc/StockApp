import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginResponseDto } from '../../models/login-response-dto.model';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  public isLogin: boolean = false;

  public user: LoginResponseDto = new LoginResponseDto();

  constructor(private router: Router, private authenticationService: AuthenticationService) {
    this.user = this.authenticationService.getUser();
  }

  logout() {
    this.authenticationService.removeUser().subscribe(
      (data) => {
        if (data.response) {
          window.location.href = "/";
        }
      },
      (error) => {
        console.log(error);
      });
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
