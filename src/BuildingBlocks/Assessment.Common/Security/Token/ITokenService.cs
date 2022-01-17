using Assessment.Dto.Stock.Authentication;
using System.Threading.Tasks;

namespace Assessment.Common.Security.Token
{
    public interface ITokenService
    {
        string CreateToken(string username);
        Task<LoginResponseDto> GetUserByTokenAsync(string token);
        LoginResponseDto GetUserByToken(string token);
        Task<bool> ValidateTokenAsync(string token);

        Task RefreshTokenAsync(string token);
        Task<LoginResponseDto> WriteTokenAsync(LoginResponseDto loginResponseDto);

        Task RemoveTokenAsync(string token);
    }
}
