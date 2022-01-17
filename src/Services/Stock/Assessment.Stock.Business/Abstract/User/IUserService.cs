
using Assessment.Dto.Stock.User;
using System.Threading.Tasks;

namespace Assessment.Stock.Business.Abstract.UserService
{
    public interface IUserService
    {
        UserResponseDto GetUserByUsername(string username);
        Task<UserResponseDto> GetUserByUsernameAsync(string username);
        Task<UserResponseDto> UpdateAsync(UserUpdateDto userUpdateDto);
    }
}
