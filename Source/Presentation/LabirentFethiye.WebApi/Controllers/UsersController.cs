using LabirentFethiye.Application.Abstracts.GlobalInterfaces;
using LabirentFethiye.Common.Dtos.GlobalDtos.UserDtos.UserRequestDtos;
using LabirentFethiye.Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LabirentFethiye.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Admin")]
    public class UsersController : BaseController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] UserGetAllRequestDto requestModel)
        {
            try
            {
                var users = await userService.GetAll(requestModel);
                return StatusCode(users.StatusCode, users);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm, FromBody] UserCreateRequestDto model)
        {
            try
            {
                var userCreate = await userService.Create(model);
                return StatusCode(userCreate.StatusCode, userCreate);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        public async Task<IActionResult> UserRoleAsign([FromForm, FromBody] UserRoleAsignRequestDto model)
        {
            try
            {
                var userRoleAsign = await userService.UserRoleAsign(model);
                return StatusCode(userRoleAsign.StatusCode, userRoleAsign);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }
    }
}
