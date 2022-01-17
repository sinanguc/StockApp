using Assessment.Common.Infrastructure.ErrorHandling;
using Assessment.Common.Security.Token;
using Assessment.Dto.Stock.Authentication;
using Assessment.Dto.Stock.User;
using Assessment.Enum.Stock.Messages;
using Assessment.Stock.Business.Abstract.AuthenticationService;
using Assessment.Stock.Business.Abstract.UserService;
using Assessment.Stock.DataAccess.Abstract;
using AutoMapper;
using System.Threading.Tasks;

namespace Assessment.Stock.Business.Concrete.AuthenticationManager
{
    public class AuthenticationManager : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthenticationManager(IMapper mapper, IUnitOfWork unitOfWork, IUserService userService, ITokenService tokenService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userService = userService;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDto> CreateAccessTokenAsync(LoginResponseDto loginResponseDto)
        {
            return await _tokenService.WriteTokenAsync(loginResponseDto);
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var user = await _userService.GetUserByUsernameAsync(loginRequestDto.UserName);
            if (user == null)
                throw new RecordNotFoundException(StockMessages.KayitBulunamadi);

            //TODO: User şifre veri tabanında hashli olmalı ve burada hashli şekilde kontrol edilmeli
            if (!user.Password.Equals(loginRequestDto.Password))
                throw new LoginIncorrectException(StockMessages.KullaniciAdiveyaSifreHatali);

            var mappedUser = _mapper.Map<LoginResponseDto>(user);
            await CreateAccessTokenAsync(mappedUser);

            await _userService.UpdateAsync(_mapper.Map<UserUpdateDto>(mappedUser));

            return mappedUser;
        }

        public async Task LogoutAsync(string token)
        {
            await _tokenService.RemoveTokenAsync(token);
        }

    }
}
