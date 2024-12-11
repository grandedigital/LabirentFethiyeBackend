using LabirentFethiye.Application.Abstracts.ProjectInterfaces;
using LabirentFethiye.Common.Dtos.ProjectDtos.ReservationInfoDtos.ReservationInfoRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.ReservationInfoDtos.ReservationInfoResponseDtos;
using LabirentFethiye.Common.Enums;
using LabirentFethiye.Common.Responses;
using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LabirentFethiye.Persistence.Concrete.ProjectConcretes
{
    public class ReservationInfoService : IReservationInfoService
    {
        private readonly AppDbContext context;

        public ReservationInfoService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<ResponseDto<BaseResponseDto>> Create(ReservationInfoCreateRequestDto model, Guid userId)
        {
            try
            {
                var isOwner = await context.ReservationInfos.AnyAsync(x => x.ReservationId == model.ReservationId && x.Owner == true);
                if (isOwner && model.Owner) { return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Create Errors..", Description = "Her Rezervasyonun 1 sahibi olabilir.." } }, 400); }

                ReservationInfo reservationInfo = new()
                {
                    CreatedAt = DateTime.Now,
                    CreatedById = userId,
                    ReservationId = model.ReservationId,
                    IdNo = model.IdNo,
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    Phone = model.Phone,
                    PeopleType = PeopleType.Adult,
                    GeneralStatusType = GeneralStatusType.Active,
                    Owner = model.Owner
                };

                await context.ReservationInfos.AddAsync(reservationInfo);
                await context.SaveChangesAsync();
                //------
                return ResponseDto<BaseResponseDto>.Success(new() { Id = reservationInfo.Id }, 200);

            }
            catch (Exception ex) { return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500); }
        }

        public async Task<ResponseDto<BaseResponseDto>> DeleteHard(Guid Id, Guid userId)
        {
            try
            {
                var getReservationInfo = await context.ReservationInfos.FirstOrDefaultAsync(x => x.Id == Id);
                if (getReservationInfo != null)
                {
                    if (getReservationInfo.Owner == true) { return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = "Rezervasyon Sahibi Silinemez.." } }, 500); }
                    context.ReservationInfos.Remove(getReservationInfo);
                    await context.SaveChangesAsync();
                    return ResponseDto<BaseResponseDto>.Success(new() { Id = Id }, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = "Rezervasyon Info Bulunamadı.." } }, 400);
            }
            catch (Exception ex) { return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500); }
        }

        public async Task<ResponseDto<ReservationInfoGetResponseDto>> Get(Guid Id)
        {
            try
            {
                var getReservationInfo = await context.ReservationInfos
                    .Where(x => x.Id == Id)
                    .Select(reservationInfo => new ReservationInfoGetResponseDto()
                    {
                        Email = reservationInfo.Email,
                        IdNo = reservationInfo.IdNo,
                        Name = reservationInfo.Name,
                        Owner = reservationInfo.Owner,
                        PeopleType = reservationInfo.PeopleType,
                        Phone = reservationInfo.Phone,
                        Surname = reservationInfo.Surname,
                        ReservationId = reservationInfo.ReservationId
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                return ResponseDto<ReservationInfoGetResponseDto>.Success(getReservationInfo, 200);

            }
            catch (Exception ex) { return ResponseDto<ReservationInfoGetResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500); }
        }

        public async Task<ResponseDto<ICollection<ReservationInfoGetAllResponseDto>>> GetAll(Guid ReservationId)
        {
            try
            {
                var getAllReservationInfo = await context.ReservationInfos
                    .Where(x => x.ReservationId == ReservationId && x.GeneralStatusType == GeneralStatusType.Active)
                    .Select(reservationInfo => new ReservationInfoGetAllResponseDto()
                    {
                        Id = reservationInfo.Id,
                        Email = reservationInfo.Email,
                        IdNo = reservationInfo.IdNo,
                        Name = reservationInfo.Name,
                        Owner = reservationInfo.Owner,
                        PeopleType = reservationInfo.PeopleType,
                        Phone = reservationInfo.Phone,
                        Surname = reservationInfo.Surname,
                        ReservationId = reservationInfo.ReservationId
                    })
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<ReservationInfoGetAllResponseDto>>.Success(getAllReservationInfo, 200);
            }
            catch (Exception ex) { return ResponseDto<ICollection<ReservationInfoGetAllResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500); }
        }

        public async Task<ResponseDto<BaseResponseDto>> Update(ReservationInfoUpdateRequestDto model, Guid userId)
        {
            try
            {
                var getReservationInfo = await context.ReservationInfos.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (getReservationInfo != null)
                {
                    getReservationInfo.UpdatedAt = DateTime.Now;
                    getReservationInfo.UpdatedById = userId;

                    if ((getReservationInfo.IdNo != model.IdNo) && !String.IsNullOrEmpty(model.IdNo)) getReservationInfo.IdNo = model.IdNo;
                    if ((getReservationInfo.Name != model.Name) && !String.IsNullOrEmpty(model.Name)) getReservationInfo.Name = model.Name;
                    if ((getReservationInfo.Surname != model.Surname) && !String.IsNullOrEmpty(model.Surname)) getReservationInfo.Surname = model.Surname;
                    if ((getReservationInfo.Email != model.Email) && !String.IsNullOrEmpty(model.Email)) getReservationInfo.Email = model.Email;
                    if ((getReservationInfo.Phone != model.Phone) && !String.IsNullOrEmpty(model.Phone)) getReservationInfo.Phone = model.Phone;

                    if ((getReservationInfo.PeopleType != model.PeopleType) && model.PeopleType > 0) getReservationInfo.PeopleType = model.PeopleType;

                    context.ReservationInfos.Update(getReservationInfo);
                    var result = await context.SaveChangesAsync();

                    BaseResponseDto baseResponse = new() { Id = getReservationInfo.Id };
                    return ResponseDto<BaseResponseDto>.Success(baseResponse, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = "Reservation Info Bulunamadı.." } }, 400);
            }
            catch (Exception ex) { return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500); }
        }
    }
}
