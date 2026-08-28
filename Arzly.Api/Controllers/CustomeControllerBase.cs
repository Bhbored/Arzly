using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Arzly.Api.Controllers
{
    [Route("arzly/v{version:apiVersion}/[controller]")]
    [ApiController]
    [EnableRateLimiting("writes")]
    public class CustomeControllerBase : ControllerBase
    {
    }
}
