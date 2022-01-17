import { HttpClient, HttpErrorResponse, HttpHeaders, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { environment } from "../environments/environment";
import { GenericResult } from "../models/generic-result.model";
import { SalesRequestDto } from "../models/sales-request-dto.model";
import { SalesResponseDto } from "../models/sales-response-dto.model";
import { AlertifyService } from "./alertify.service";
import { AuthenticationService } from "./authentication.service";


@Injectable()
export class IntegrationService {

  integrationUrl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': '*/*'
    })
  };

  constructor(private httpClient: HttpClient, private authenticationService: AuthenticationService, private alertfy: AlertifyService) {
    this.integrationUrl = environment.WebApiUrl + '/api/Integration';
  }

  getSales(model: SalesRequestDto) {
    model.apiCode = this.authenticationService.getUser().apiCode;
    console.log(model);
    return this.httpClient.post<GenericResult<SalesResponseDto[]>>(this.integrationUrl + '/GetSales', model, this.httpOptions)
      .pipe(
        map((response: GenericResult<SalesResponseDto[]>) => {

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
