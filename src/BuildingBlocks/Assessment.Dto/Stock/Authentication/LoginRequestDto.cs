using FluentValidation;

namespace Assessment.Dto.Stock.Authentication
{
    public class LoginRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestDtoValidator()
        {
            RuleFor(v => v.UserName)
                .NotEmpty().WithMessage("Kullanıcı Adı boş olamaz")
                .Length(4, 50).WithMessage("Kullanıcı Adı 4 ile 50 karakter arasında olmalıdır");

            RuleFor(v => v.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz");
        }
    }

}
