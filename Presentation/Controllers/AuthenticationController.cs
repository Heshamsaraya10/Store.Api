using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
 
    public class AuthenticationController(IServiceManager serviceManager) : ApiController
    {

        [HttpPost("login")]
       public async Task<ActionResult<UserResultDto>> Login(LoginDto loginDto)
            =>Ok(await serviceManager.AuthenticationService.LoginAsync(loginDto));

        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDto>> Register(RegisterDto registerDto)
           => Ok(await serviceManager.AuthenticationService.RegisterAsync(registerDto));
    }
}
