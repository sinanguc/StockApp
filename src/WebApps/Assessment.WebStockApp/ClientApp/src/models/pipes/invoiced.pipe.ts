import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
  name: "invoiced"
})
export class invoicedPipe implements PipeTransform {
  transform(value: any, ...args: any[]) {
    return value ? "Invoiced" : "Not Invoiced";
  }
}
