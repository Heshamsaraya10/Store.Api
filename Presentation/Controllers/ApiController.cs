using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;
using Shared.ProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ValidationError), (int)HttpStatusCode.BadRequest)]

    public class ApiController : ControllerBase
    {

    }
}
