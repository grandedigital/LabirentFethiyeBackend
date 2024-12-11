using LabirentFethiye.Application.Abstracts.ProjectInterfaces;
using LabirentFethiye.Common.Dtos.ProjectDtos.CategoryDtos.CategoryRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.CategoryDtos.CategoryResponseDtos;
using LabirentFethiye.Common.Responses;
using LabirentFethiye.Persistence.Validations.ProjectValidations.CategoryValidations.CaegoryRequestValidations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LabirentFethiye.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Admin")]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm, FromBody] CategoryCreateRequestDto model)
        {
            try
            {
                var validator = new CategoryCreateRequestDtoValidator();
                var validationResult = validator.Validate(model);
                //if (!validationResult.IsValid)
                //{
                //    //return badrequest(validationresult.errors);
                //    List<ErrorDto> errors = new List<ErrorDto>();
                //    foreach (var item in validationResult.Errors)
                //    {
                //        errors.Add(new() { Title = item.PropertyName, Description = item.ErrorMessage });
                //    }
                //    return ResponseDto<BaseResponseDto>.Fail(errors, 400);
                //}
                var category = await categoryService.Create(model, UserId);
                return StatusCode(category.StatusCode, category);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDetail([FromForm, FromBody] CategoryDetailCreateRequestDto model)
        {
            try
            {
                var category = await categoryService.CreateDetail(model, UserId);
                return StatusCode(category.StatusCode, category);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm, FromBody] CategoryUpdateRequestDto model)
        {
            try
            {
                var category = await categoryService.Update(model, UserId);
                return StatusCode(category.StatusCode, category);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDetail([FromForm, FromBody] CategoryDetailUpdateRequestDto model)
        {
            try
            {
                var category = await categoryService.UpdateDetail(model, UserId);
                return StatusCode(category.StatusCode, category);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] Guid Id)
        {
            try
            {
                var company = await categoryService.Get(Id);
                return StatusCode(company.StatusCode, company);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<CategoryGetResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] CategoryGetAllRequestDto model)
        {
            try
            {
                var categories = await categoryService.GetAll(model);
                return StatusCode(categories.StatusCode, categories);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }
    }
}
