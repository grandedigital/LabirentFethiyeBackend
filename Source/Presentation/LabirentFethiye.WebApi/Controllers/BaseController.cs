using Microsoft.AspNetCore.Mvc;



namespace LabirentFethiye.WebApi.Controllers
{
    [Route("api/[controller]/[action]/{Id?}")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public Guid UserId
        {
            get
            {
                return Guid.Parse(HttpContext.User.Claims.ToList()[0].Value);
            }
        }
    }
}
