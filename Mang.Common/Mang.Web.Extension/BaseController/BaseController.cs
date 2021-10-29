using Microsoft.AspNetCore.Mvc;

namespace Mang.Web.Extension.BaseController
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseController : ControllerBase
    {
    }
}