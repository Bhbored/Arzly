using Microsoft.AspNetCore.Mvc;

namespace Arzly.Api.Controllers
{
    [Route("arzly/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CustomeControllerBase : ControllerBase
    {
    }
}
