using LabirentFethiye.Application.Abstracts.WebSiteInterfaces;
using LabirentFethiye.Common.Dtos.WebSiteDtos.MenuDtos.MenuRequestDtos;
using LabirentFethiye.Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LabirentFethiye.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Admin")]
    public class MenusController : BaseController
    {
        private readonly IMenuService menuService;
        public MenusController(IMenuService menuService)
        {
            this.menuService = menuService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] Guid Id)
        {
            try
            {
                var distanceRuler = await menuService.Get(Id);
                return StatusCode(distanceRuler.StatusCode, distanceRuler);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMenuBySlug([FromQuery] string slug)
        {
            try
            {
                var distanceRuler = await menuService.GetMenuBySlug(slug);
                return StatusCode(distanceRuler.StatusCode, distanceRuler);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var menus = await menuService.GetAll();
                return StatusCode(menus.StatusCode, menus);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm, FromBody] MenuCreateRequestDto model)
        {
            try
            {
                var webPage = await menuService.Create(model, UserId);
                return StatusCode(webPage.StatusCode, webPage);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDetail([FromForm, FromBody] MenuDetailCreateRequestDto model)
        {
            try
            {
                var menuDetail = await menuService.CreateDetail(model, UserId);
                return StatusCode(menuDetail.StatusCode, menuDetail);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm, FromBody] MenuUpdateRequestDto model)
        {
            try
            {
                var distanceRuler = await menuService.Update(model, UserId);
                return StatusCode(distanceRuler.StatusCode, distanceRuler);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDetail([FromForm, FromBody] MenuDetailUpdateRequestDto model)
        {
            try
            {
                var distanceRulerDetail = await menuService.UpdateDetail(model, UserId);
                return StatusCode(distanceRulerDetail.StatusCode, distanceRulerDetail);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteHard([FromRoute] Guid Id)
        {
            try
            {
                var photos = await menuService.DeleteHard(Id);
                return StatusCode(photos.StatusCode, photos);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }
    }
}
