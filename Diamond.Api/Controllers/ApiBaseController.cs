using Microsoft.AspNetCore.Mvc;

namespace Diamond.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public abstract class ApiBaseController : ControllerBase
    {

    }
}
