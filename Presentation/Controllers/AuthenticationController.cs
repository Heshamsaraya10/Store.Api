using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static NHibernate.Engine.Query.CallableParser;

namespace Presentation.Controllers
{
 
    public class AuthenticationController(IServiceManager serviceManager) : ApiController
    {

        [HttpPost]
       public async Task<ActionResult<UserResultDto>> Login(LoginDto loginDto)
            =>Ok(await serviceManager.AuthenticationService.LoginAsync(loginDto));

        [HttpPost]
        public async Task<ActionResult<UserResultDto>> Register(RegisterDto registerDto)
           => Ok(await serviceManager.AuthenticationService.RegisterAsync(registerDto));

        [HttpGet]
        public async Task<ActionResult<bool>> IsEmailExist(string email)
           => Ok(await serviceManager.AuthenticationService.IsEmailExist(email));

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserResultDto>> GetCurrentUser()
        {
             var email = User.FindFirst(ClaimTypes.Email).Value;
            
            var result = await serviceManager.AuthenticationService.GetUserByEmailAsync(email);

            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IdentityAddressDto>> GetUserAddress()
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;

            var result = await serviceManager.AuthenticationService.GetUserAddressAsync(email);

            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<IdentityAddressDto>> UpdateUserAddress(IdentityAddressDto addressDto)
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;

            var result = await serviceManager.AuthenticationService.UpdateUserAddressAsync(email , addressDto);

            return Ok(result);
        }

    }
}
