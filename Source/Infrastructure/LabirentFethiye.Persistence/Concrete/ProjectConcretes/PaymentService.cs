using LabirentFethiye.Application.Abstracts.ProjectInterfaces;
using LabirentFethiye.Common.Dtos.ProjectDtos.PaymentDtos.PaymentRequestDtos;
using LabirentFethiye.Common.Enums;
using LabirentFethiye.Common.Responses;
using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LabirentFethiye.Persistence.Concrete.ProjectConcretes
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext context;

        public PaymentService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<ResponseDto<BaseResponseDto>> Create(PaymentCreateRequestDto model, Guid userId)
        {
            try
            {
                Payment payment = new()
                {
                    Amount = model.Amount,
                    Description = model.Description,
                    PaymentTypeId = model.PaymentTypeId,
                    InOrOut = model.InOrOut,
                    PriceType = model.PriceType,
                    ReservationId = model.ReservationId,
                    HotelId = model.HotelId,
                    VillaId = model.VillaId,
                    RoomId = model.RoomId,
                    GeneralStatusType = GeneralStatusType.Active,
                    CreatedAt = DateTime.Now,
                    CreatedById = userId
                };

                await context.Payments.AddAsync(payment);
                await context.SaveChangesAsync();

                return ResponseDto<BaseResponseDto>.Success(new() { Id = payment.Id }, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<PaymentGetResponseDto>> Get(Guid Id)
        {
            try
            {
                var getPayment = await context.Payments
                    .Where(x => x.Id == Id)
                    .Select(payment => new PaymentGetResponseDto()
                    {
                        Amount = payment.Amount,
                        Description = payment.Description,
                        InOrOut = payment.InOrOut,
                        PaymentTypeId = payment.PaymentTypeId,
                        PriceType = payment.PriceType,
                        PaymentType = new PaymentGetAllResponseDtoPaymentType()
                        {
                            Title = payment.PaymentType.Title
                        }

                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                return ResponseDto<PaymentGetResponseDto>.Success(getPayment, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<PaymentGetResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<PaymentGetAllResponseDto>>> GetAll(PaymentGetAllRequestDto model)
        {
            try
            {
                var query = context.Payments.AsNoTracking().AsQueryable().Where(x => x.GeneralStatusType == GeneralStatusType.Active);

                if (model.ReservationId != null)
                    query = query.Where(x => x.ReservationId == model.ReservationId);
                else if (model.HotelId != null)
                    query = query.Where(x => x.HotelId == model.HotelId);
                else if (model.RoomId != null)
                    query = query.Where(x => x.RoomId == model.RoomId);
                else if (model.VillaId != null)
                    query = query.Where(x => x.VillaId == model.VillaId);

                query = query
                    .Skip(model.Pagination.Page * model.Pagination.Size)
                    .Take(model.Pagination.Size);

                var getAllPayment = await query
                     .Select(payment => new PaymentGetAllResponseDto()
                     {
                         Id = payment.Id,
                         Amount = payment.Amount,
                         CreatedAt = DateTime.Now,
                         Description = payment.Description,
                         InOrOut = payment.InOrOut,
                         PriceType = payment.PriceType,
                         PaymentTypeId = payment.PaymentTypeId,
                         PaymentType = new PaymentGetAllResponseDtoPaymentType()
                         {
                             Title = payment.PaymentType.Title
                         }
                     })
                     .ToListAsync();

                return ResponseDto<ICollection<PaymentGetAllResponseDto>>.Success(getAllPayment, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<PaymentGetAllResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> Update(PaymentUpdateRequestDto model, Guid userId)
        {
            try
            {
                var getPayment = await context.Payments.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (getPayment != null)
                {
                    getPayment.UpdatedAt = DateTime.Now;
                    getPayment.UpdatedById = userId;

                    if ((getPayment.Amount != model.Amount) && model.Amount > 0) getPayment.Amount = model.Amount;
                    if ((getPayment.InOrOut != model.InOrOut)) getPayment.InOrOut = model.InOrOut;
                    if ((getPayment.Description != model.Description) && model.Description is not null) getPayment.Description = model.Description;
                    if ((getPayment.GeneralStatusType != model.GeneralStatusType) && model.GeneralStatusType > 0) getPayment.GeneralStatusType = model.GeneralStatusType;
                    if ((getPayment.PriceType != model.PriceType) && model.PriceType > 0) getPayment.PriceType = model.PriceType;
                    if ((getPayment.PaymentTypeId != model.PaymentTypeId) && model.PaymentTypeId != Guid.Empty) getPayment.PaymentTypeId = model.PaymentTypeId;

                    context.Payments.Update(getPayment);
                    var result = await context.SaveChangesAsync();

                    return ResponseDto<BaseResponseDto>.Success(new() { Id = getPayment.Id }, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Payment Bulunamadı..", Description = "Aradığınız Payment Bulunamadı.." } }, 400);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }
    }
}
