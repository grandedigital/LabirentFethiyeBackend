using LabirentFethiye.Application.Abstracts.ProjectInterfaces;
using LabirentFethiye.Common.Dtos.ProjectDtos.PaymentTypeDtos.PaymentTypeRequestDtos;
using LabirentFethiye.Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LabirentFethiye.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Admin")]
    public class PaymentTypesController : BaseController
    {
        private readonly IPaymentTypeService paymentTypeService;

        public PaymentTypesController(IPaymentTypeService paymentTypeService)
        {
            this.paymentTypeService = paymentTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] Guid Id)
        {
            try
            {
                var company = await paymentTypeService.Get(Id);
                return StatusCode(company.StatusCode, company);
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
                var categories = await paymentTypeService.GetAll();
                return StatusCode(categories.StatusCode, categories);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm, FromBody] PaymentTypeCreateRequestDto model)
        {
            try
            {
                var category = await paymentTypeService.Create(model, UserId);
                return StatusCode(category.StatusCode, category);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm, FromBody] PaymentTypeUpdateRequestDto model)
        {
            try
            {
                var category = await paymentTypeService.Update(model, UserId);
                return StatusCode(category.StatusCode, category);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            try
            {
                var category = await paymentTypeService.Delete(Id, UserId);
                return StatusCode(category.StatusCode, category);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }
    }
}
