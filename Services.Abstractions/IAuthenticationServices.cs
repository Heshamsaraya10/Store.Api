using Shared.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IAuthenticationServices
    {
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
        Task<UserResultDto> RegisterAsync(RegisterDto  registerDto);
        Task<UserResultDto> GetUserByEmailAsync(string email);
        Task<bool> IsEmailExist(string email);

        Task<IdentityAddressDto> GetUserAddressAsync(string email);
        Task<IdentityAddressDto> UpdateUserAddressAsync(string email , IdentityAddressDto addressDto);




    }
} 
