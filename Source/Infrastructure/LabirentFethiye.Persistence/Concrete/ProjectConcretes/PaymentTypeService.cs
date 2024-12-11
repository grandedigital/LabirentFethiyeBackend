using LabirentFethiye.Application.Abstracts.ProjectInterfaces;
using LabirentFethiye.Common.Dtos.ProjectDtos.PaymentTypeDtos.PaymentTypeRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.PaymentTypeDtos.PaymentTypeResponseDtos;
using LabirentFethiye.Common.Enums;
using LabirentFethiye.Common.Responses;
using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LabirentFethiye.Persistence.Concrete.ProjectConcretes
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly AppDbContext context;

        public PaymentTypeService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<ResponseDto<BaseResponseDto>> Create(PaymentTypeCreateRequestDto model, Guid userId)
        {
            try
            {
                PaymentType paymentType = new()
                {
                    Title = model.Title,
                    Description = model.Description,
                    GeneralStatusType = GeneralStatusType.Active,
                    CreatedAt = DateTime.Now,
                    CreatedById = userId
                };

                await context.PaymentTypes.AddAsync(paymentType);
                await context.SaveChangesAsync();

                return ResponseDto<BaseResponseDto>.Success(new() { Id = paymentType.Id }, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> Delete(Guid Id, Guid userId)
        {
            try
            {
                // Todo: Bu Silme servisi iptal edildi..
                //var getPaymentType = await context.PaymentTypes.FirstOrDefaultAsync(x => x.Id == Id);
                //if (getPaymentType != null)
                //{
                //    getPaymentType.UpdatedAt = DateTime.Now;
                //    getPaymentType.UpdatedById = userId;
                //    getPaymentType.GeneralStatusType = GeneralStatusType.Deleted;

                //    context.PaymentTypes.Update(getPaymentType);
                //    var result = await context.SaveChangesAsync();

                //    return ResponseDto<BaseResponseDto>.Success(new() { Id = Id }, 200);
                //}
                //else
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "PaymentType Bulunamadı..", Description = "Aradığınız PaymentType Bulunamadı.." } }, 400);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<PaymentTypeGetResponseDto>> Get(Guid Id)
        {
            try
            {
                var getPaymentType = await context.PaymentTypes
                    .Where(x => x.Id == Id)
                    .Select(paymentType => new PaymentTypeGetResponseDto()
                    {
                        Description = paymentType.Description,
                        Title = paymentType.Title,
                        Payments = paymentType.Payments.Select(payment => new PaymentTypeGetResponseDtoPayment()
                        {
                            Amount = payment.Amount,
                            Description = payment.Description,
                            InOrOut = payment.InOrOut
                        }).ToList()

                    }).FirstOrDefaultAsync();

                return ResponseDto<PaymentTypeGetResponseDto>.Success(getPaymentType, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<PaymentTypeGetResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<PaymentTypeGetAllResponseDto>>> GetAll()
        {
            try
            {
                var getAllPaymentType = await context.PaymentTypes
                  .Where(x => x.GeneralStatusType == GeneralStatusType.Active)
                  .Select(payment => new PaymentTypeGetAllResponseDto()
                  {
                      Id = payment.Id,
                      Title = payment.Title,
                      Description = payment.Description
                  })
                  .AsNoTracking()
                  .ToListAsync();

                return ResponseDto<ICollection<PaymentTypeGetAllResponseDto>>.Success(getAllPaymentType, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<PaymentTypeGetAllResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> Update(PaymentTypeUpdateRequestDto model, Guid userId)
        {
            try
            {
                var getPaymentType = await context.PaymentTypes.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (getPaymentType != null)
                {
                    getPaymentType.UpdatedAt = DateTime.Now;
                    getPaymentType.UpdatedById = userId;

                    if ((getPaymentType.Title != model.Title) && model.Title is not null) getPaymentType.Title = model.Title;
                    if ((getPaymentType.Description != model.Description) && model.Description is not null) getPaymentType.Description = model.Description;
                    if ((getPaymentType.GeneralStatusType != model.GeneralStatusType) && model.GeneralStatusType > 0) getPaymentType.GeneralStatusType = model.GeneralStatusType;

                    context.PaymentTypes.Update(getPaymentType);
                    var result = await context.SaveChangesAsync();

                    return ResponseDto<BaseResponseDto>.Success(new() { Id = getPaymentType.Id }, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "PaymentType Bulunamadı..", Description = "Aradığınız PaymentType Bulunamadı.." } }, 400);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }
    }
}
