using Assessment.Common.Infrastructure.ErrorHandling;
using Assessment.Dto.Stock.User;
using Assessment.Enum.Stock.Messages;
using Assessment.Stock.Business.Abstract.UserService;
using Assessment.Stock.DataAccess.Abstract;
using Assessment.Stock.Entities.Concrete.Users;
using AutoMapper;
using System.Threading.Tasks;

namespace Assessment.Stock.Business.Concrete.UserManager
{
    public class UserManager : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserManager(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public UserResponseDto GetUserByUsername(string username)
        {
            var user = _unitOfWork.UserRepository.FirstOrDefault(d => d.Username == username);

            if (user == null)
                throw new RecordExistException(StockMessages.KayitBulunamadi);

            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<UserResponseDto> GetUserByUsernameAsync(string username)
        {
            var user = await _unitOfWork.UserRepository.FirstOrDefaultAsync(d => d.Username == username);

            if (user == null)
                throw new RecordExistException(StockMessages.KayitBulunamadi);

            var mappedUser = _mapper.Map<UserResponseDto>(user);
            mappedUser.UserId = user.Id;
            return mappedUser;
        }

        public async Task<UserResponseDto> UpdateAsync(UserUpdateDto userUpdateDto)
        {
            var user = await _unitOfWork.UserRepository.FirstOrDefaultAsync(d => d.Id == userUpdateDto.UserId);
            if (user == null)
                throw new RecordNotFoundException(StockMessages.KayitBulunamadi);

            var mappedUser = _mapper.Map(userUpdateDto, user);
            //mappedUser.Id = user.Id;

            _unitOfWork.UserRepository.Update(mappedUser);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map(mappedUser, new UserResponseDto());
        }
    }
}
