using LabirentFethiye.Application.Abstracts.WebSiteInterfaces;
using LabirentFethiye.Common.Dtos.WebSiteDtos.WebPageDtos.WebPageRequestDto;
using LabirentFethiye.Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LabirentFethiye.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Admin")]

    public class WebPagesController : BaseController
    {
        private readonly IWebPageService webPageService;

        public WebPagesController(IWebPageService webPageService)
        {
            this.webPageService = webPageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] Guid Id)
        {
            try
            {
                var webPage = await webPageService.Get(Id);
                return StatusCode(webPage.StatusCode, webPage);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] WebPageGetAllRequstDtos model)
        {
            try
            {
                var webPages = await webPageService.GetAll(model);
                return StatusCode(webPages.StatusCode, webPages);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm, FromBody] WebPageCreateRequestDto model)
        {
            try
            {
                var webPage = await webPageService.Create(model, UserId);
                return StatusCode(webPage.StatusCode, webPage);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDetail([FromForm, FromBody] WebPageDetailCreateRequestDto model)
        {
            try
            {
                var webPageDetail = await webPageService.CreateDetail(model, UserId);
                return StatusCode(webPageDetail.StatusCode, webPageDetail);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm, FromBody] WebPageUpdateRequestDto model)
        {
            try
            {
                var distanceRuler = await webPageService.Update(model, UserId);
                return StatusCode(distanceRuler.StatusCode, distanceRuler);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDetail([FromForm, FromBody] WebPageDetailUpdateRequestDto model)
        {
            try
            {
                var distanceRulerDetail = await webPageService.UpdateDetail(model, UserId);
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
                var photos = await webPageService.DeleteHard(Id);
                return StatusCode(photos.StatusCode, photos);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        //---

        [HttpGet]
        public async Task<IActionResult> GetAllWebPhoto([FromQuery] Guid WebPageId)
        {
            try
            {
                var webPhotos = await webPageService.GetAllWebPhoto(WebPageId);
                return StatusCode(webPhotos.StatusCode, webPhotos);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePhoto([FromForm] WebPhotoCreateRequestDto model)
        {
            try
            {
                var photo = await webPageService.CreatePhoto(model, UserId);
                return StatusCode(photo.StatusCode, photo);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteHardWebPhoto([FromRoute] Guid Id)
        {
            try
            {
                var photos = await webPageService.DeleteHardWebPhoto(Id);
                return StatusCode(photos.StatusCode, photos);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLine([FromForm] List<WebPhotoUpdateLineRequestDto> model)
        {
            try
            {
                var photo = await webPageService.UpdateLine(model, UserId);
                return StatusCode(photo.StatusCode, photo);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }
    }
}
