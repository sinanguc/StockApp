import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators";
import { LoginRequestDto } from '../../models/login-request-dto.model';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  model: LoginRequestDto = new LoginRequestDto();

  constructor(private router: Router,
    private authenticationService: AuthenticationService) {

    if (this.authenticationService.getUser()) {
      window.location.href = "/sales";
    }

  }


  login() {
    this.authenticationService.signIn(this.model).subscribe(
      (data) => {
        if (data.result) {
          window.location.href = "/sales";
        }
      },
      (error) => {
        console.log(error);
      });
  }
}



