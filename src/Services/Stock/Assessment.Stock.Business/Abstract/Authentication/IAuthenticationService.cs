using Assessment.Dto.Stock.Authentication;
using System.Threading.Tasks;

namespace Assessment.Stock.Business.Abstract.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<LoginResponseDto> CreateAccessTokenAsync(LoginResponseDto loginResponseDto);

        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);

        Task LogoutAsync(string token);
    }
}
