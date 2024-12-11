using LabirentFethiye.Application.Abstracts.GlobalInterfaces;
using LabirentFethiye.Common.Dtos.GlobalDtos.AuthDtos.AuthRequestDtos;
using Microsoft.AspNetCore.Mvc;

namespace LabirentFethiye.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthsController: ControllerBase
    {
        private readonly IAuthService authService;
        public AuthsController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm, FromBody] LoginRequestDto requestModel)
        {
            try
            {
                var response = await authService.LoginAsync(requestModel);
                return Ok(response);
            }
            catch (Exception ex) { throw new Exception("Login | " + ex.ToString()); }
        }

    }
}
