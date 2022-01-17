import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { EnumInvoiced } from '../../models/enums/invoiced-info.enum';
import { invoicedPipe } from '../../models/pipes/invoiced.pipe';
import { SalesRequestDto } from '../../models/sales-request-dto.model';
import { SalesResponseDto } from '../../models/sales-response-dto.model';
import { IntegrationService } from '../../services/integration.service';

@Component({
  selector: 'app-sales',
  templateUrl: './sales.component.html',
  providers: []
})
export class SalesComponent {
  sales: SalesResponseDto[];

  model: SalesRequestDto = new SalesRequestDto();

  constructor(private integrationService: IntegrationService) {
    this.model.storeId = 37814;
    this.model.orderStatus = "Completed";
    this.model.invoiceStatus = 0;
    this.getSales();
  }

  getSales() {
    this.integrationService.getSales(this.model).subscribe(
      (data) => {
        if (data.result) {
          this.sales = data.response;
        }
      }, (error) => {
        console.log(error);
      });
  }

}
