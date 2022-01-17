using Assessment.Dto.Stock.Authentication;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Assessment.Common.Security.Token
{
    public class TokenManager : ITokenService
    {
        private readonly IDistributedCache _redisCache;

        public TokenManager(IDistributedCache cache)
        {
            _redisCache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public string CreateToken(string username)
        {
            return $"{Guid.NewGuid()}_{username}";
        }

        public async Task<LoginResponseDto> GetUserByTokenAsync(string token)
        {
            string userInfo = await _redisCache.GetStringAsync(token);
            if (string.IsNullOrEmpty(userInfo))
                return null;
            return JsonConvert.DeserializeObject<LoginResponseDto>(userInfo);
        }

        public LoginResponseDto GetUserByToken(string token)
        {
            string userInfo = _redisCache.GetString(token);
            if (string.IsNullOrEmpty(userInfo))
                return null;
            return JsonConvert.DeserializeObject<LoginResponseDto>(userInfo);
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            return await GetUserByTokenAsync(token) != null;
        }

        public async Task RefreshTokenAsync(string token)
        {
            await _redisCache.RefreshAsync(token);
        }

        public async Task<LoginResponseDto> WriteTokenAsync(LoginResponseDto loginResponseDto)
        {
            loginResponseDto.ApiCode = CreateToken(loginResponseDto.Username);
            loginResponseDto.PackageEndDate = DateTime.Now.AddHours(15);
            await _redisCache.SetStringAsync(loginResponseDto.ApiCode, JsonConvert.SerializeObject(loginResponseDto), 
                new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(15) });
            return loginResponseDto;
        }

        public async Task RemoveTokenAsync(string token)
        {
            await _redisCache.RemoveAsync(token);
        }
    }
}
