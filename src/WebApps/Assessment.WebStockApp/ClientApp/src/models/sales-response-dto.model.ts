export class SalesResponseDto {
  id: string;
  storeId: string;
  productResponseDto: ProductResponseDto;
  orderProductCount: string;
  amount: string;
  invoiceResponseDto: InvoiceResponseDto;
  invoiceStatus: string;
  orderStatus: string;
  orderTime: string;
}



export class ProductResponseDto {
  id: string;
  name: string;
  stock: string;
  price: string;
}

export class InvoiceResponseDto {
  id: string;
  productId: string;
  customerName: string;
  customerSurname: string;
  amount: string;
  invoiceDate: string;
}
