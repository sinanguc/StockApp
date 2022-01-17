import { HttpClient, HttpErrorResponse, HttpHeaders, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { environment } from "../environments/environment";
import { GenericResult } from "../models/generic-result.model";
import { LogRequestDto } from "../models/log-request-dto.model";
import { LogResponseDto } from "../models/log-response-dto.model";
import { AlertifyService } from "./alertify.service";
import { AuthenticationService } from "./authentication.service";


@Injectable()
export class LogService {

  logUrl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': '*/*'
    })
  };

  constructor(private httpClient: HttpClient, private authenticationService: AuthenticationService, private alertfy: AlertifyService) {
    this.logUrl = environment.WebApiUrl + '/api/Log';
    
  }

  getLogs(model: LogRequestDto) {
    model.apiCode = this.authenticationService.getUser().apiCode;
    return this.httpClient.get<GenericResult<LogResponseDto[]>>(`${this.logUrl}/GetLogs?apiCode=${model.apiCode}&logType=${model.logType}&id=${model.id}`, this.httpOptions)
      .pipe(
        map((response: GenericResult<LogResponseDto[]>) => {

          if (response.result) {
            return response;
          } else {
            this.alertfy.error("Veriler okulanamadı");
          }

        }, (err: HttpErrorResponse) => {
          console.log(err);
        }
        ));
  }

}
