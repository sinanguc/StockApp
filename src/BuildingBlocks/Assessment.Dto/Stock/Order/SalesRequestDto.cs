using FluentValidation;

namespace Assessment.Dto.Stock.Order
{
    public class SalesRequestDto
    {
        public string ApiCode { get; set; }
        public int StoreId { get; set; }
        public string OrderStatus { get; set; }
        public int InvoiceStatus { get; set; }
    }

    public class SalesRequestDtoValidator : AbstractValidator<SalesRequestDto>
    {
        public SalesRequestDtoValidator()
        {
            RuleFor(v => v.ApiCode)
                .NotEmpty().WithMessage("ApiCode boş olamaz");

            RuleFor(v => v.StoreId)
                .NotEmpty().WithMessage("StoreId boş olamaz");

            RuleFor(v => v.OrderStatus)
                .NotEmpty().WithMessage("OrderStatus boş olamaz");

            RuleFor(v => v.InvoiceStatus)
                .GreaterThanOrEqualTo(0).WithMessage("InvoiceStatus boş olamaz");

        }
    }
}
