import { HttpClient, HttpErrorResponse, HttpHeaders, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { environment } from "../environments/environment";
import { GenericResult } from "../models/generic-result.model";
import { LoginRequestDto, LogoutRequestDto } from "../models/login-request-dto.model";
import { LoginResponseDto } from "../models/login-response-dto.model";
import { AlertifyService } from "./alertify.service";


@Injectable()
export class AuthenticationService {

  authUrl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': '*/*'
    })
  };

  constructor(private httpClient: HttpClient, private alertfy: AlertifyService) {
    this.authUrl = environment.WebApiUrl + '/api/user';
    console.log(environment.WebApiUrl);
  }

  signIn(model: LoginRequestDto) {
    return this.httpClient.post<GenericResult<LoginResponseDto>>(this.authUrl + '/dologin', model, this.httpOptions)
      .pipe(
        map((response: GenericResult<LoginResponseDto>) => {

          if (response.result) {
            localStorage.setItem('user', JSON.stringify(response.response));
            return response;
          } else {
            this.alertfy.error("Login işlemi gerçekleştirilemedi. Lütfen tekrar deneyiniz!!");

          }          

        }, (err: HttpErrorResponse) => {
          console.log(err);
        }
        ));
  }

  getUser(): LoginResponseDto {
    return JSON.parse(localStorage.getItem('user'));
  }

  removeUser() {
    console.log("girdi");
    var model: LogoutRequestDto = new LogoutRequestDto();
    model.token = this.getUser().apiCode;

    console.log(`${this.authUrl}/dologout?apiCode=${model.token}`);

    return this.httpClient.get<GenericResult<boolean>>(`${this.authUrl}/dologout?apiCode=${model.token}`, this.httpOptions)
      .pipe(
        map((response: GenericResult<boolean>) => {

          console.log(response);
          localStorage.removeItem('user');
          return response;

        }, (err: HttpErrorResponse) => {
          console.log(err);
        }
        ));

  }

}


