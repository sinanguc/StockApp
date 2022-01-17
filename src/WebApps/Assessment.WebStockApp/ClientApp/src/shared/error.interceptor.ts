import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { EnumResponseMessageType } from '../models/enums/response-message-type.enum';
import { AlertifyService } from '../services/alertify.service';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

  constructor(private alertify: AlertifyService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request)
      .pipe(
        retry(1),
        catchError((error: HttpErrorResponse) => {

          let errorMessage = '';

          if (error.error instanceof ErrorEvent) {
            // client-side error 
            errorMessage = `Hata: ${error.error.message}`;
            this.alertify.error(`Hata: ${error.error.message}`);
          } else {
            // server-side error
            errorMessage = error.error.errorMessage;
            if (error.status == 401) {
              errorMessage = error.error.Value.ErrorMessage;
            }

            this.alertify.error(`Hata: ${errorMessage}`);
          }
          return throwError(errorMessage);
        })
      )
  }
}
