import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LogResponseDto } from '../../models/log-response-dto.model';
import { LogRequestDto } from '../../models/log-request-dto.model';
import { LogService } from '../../services/log.service';

@Component({
  selector: 'app-log',
  templateUrl: './log.component.html'
})
export class LogComponent {

  logs: LogResponseDto[];

  model: LogRequestDto = new LogRequestDto();

  constructor(private logService: LogService) {
    this.model.id = "";
    this.model.logType = "Request";
    this.getLogs();
  }

  getLogs() {
    this.logService.getLogs(this.model).subscribe(
      (data) => {
        if (data.result) {
          this.logs = data.response;
        }
      }, (error) => {
        console.log(error);
      });
  }


}
