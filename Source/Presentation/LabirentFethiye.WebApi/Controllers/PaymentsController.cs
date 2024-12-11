using LabirentFethiye.Application.Abstracts.ProjectInterfaces;
using LabirentFethiye.Common.Dtos.ProjectDtos.PaymentDtos.PaymentRequestDtos;
using LabirentFethiye.Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LabirentFethiye.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Admin")]
    public class PaymentsController : BaseController
    {
        private readonly IPaymentService paymentsService;

        public PaymentsController(IPaymentService paymentsService)
        {
            this.paymentsService = paymentsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] Guid Id)
        {
            try
            {
                var company = await paymentsService.Get(Id);
                return StatusCode(company.StatusCode, company);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaymentGetAllRequestDto model)
        {
            try
            {
                var categories = await paymentsService.GetAll(model);
                return StatusCode(categories.StatusCode, categories);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm, FromBody] PaymentCreateRequestDto model)
        {
            try
            {
                var category = await paymentsService.Create(model, UserId);
                return StatusCode(category.StatusCode, category);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }


        [HttpPost]
        public async Task<IActionResult> Update([FromForm, FromBody] PaymentUpdateRequestDto model)
        {
            try
            {
                var category = await paymentsService.Update(model, UserId);
                return StatusCode(category.StatusCode, category);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }
    }
}
