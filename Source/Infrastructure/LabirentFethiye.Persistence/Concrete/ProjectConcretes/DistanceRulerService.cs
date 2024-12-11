using LabirentFethiye.Application.Abstracts.ProjectInterfaces;
using LabirentFethiye.Common.Dtos.ProjectDtos.DistanceRulerDtos.DistanceRulerRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.DistanceRulerDtos.DistanceRulerResponseDtos;
using LabirentFethiye.Common.Enums;
using LabirentFethiye.Common.Responses;
using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LabirentFethiye.Persistence.Concrete.ProjectConcretes
{
    public class DistanceRulerService: IDistanceRulerService
    {
        private readonly AppDbContext context;

        public DistanceRulerService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<ResponseDto<BaseResponseDto>> Create(DistanceRulerCreateRequestDto model, Guid userId)
        {
            try
            {
                DistanceRuler distanceRuler = new()
                {
                    Icon = model.Icon,
                    HotelId = model.HotelId,
                    VillaId = model.VillaId,
                    GeneralStatusType = GeneralStatusType.Active,
                    CreatedAt = DateTime.Now,
                    CreatedById = userId,
                    Value = model.Value,
                    DistanceRulerDetails = new List<DistanceRulerDetail>()
                    {
                        new DistanceRulerDetail()
                        {
                            LanguageCode = model.LanguageCode,
                            Name = model.Name
                        }
                    }
                };

                await context.DistanceRulers.AddAsync(distanceRuler);
                var result = await context.SaveChangesAsync();

                return ResponseDto<BaseResponseDto>.Success(new() { Id = distanceRuler.Id }, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> CreateDetail(DistanceRulerDetailCreateRequestDto model, Guid userId)
        {
            try
            {
                DistanceRulerDetail distanceRulerDetail = new()
                {
                    DistanceRulerId = model.DistanceRulerId,
                    LanguageCode = model.LanguageCode,
                    Name = model.Name,
                    CreatedById = userId
                };

                await context.DistanceRulerDetails.AddAsync(distanceRulerDetail);
                var result = await context.SaveChangesAsync();

                return ResponseDto<BaseResponseDto>.Success(new() { Id = distanceRulerDetail.Id }, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> DeleteHard(Guid Id)
        {
            // Todo : OnDelete(DeleteBehavior.Cascade) ana kayıt silinirse alt kayırlarıda silinmeli. Bu ayar yapılacak. YAPILDI TEST EDİLECEK
            try
            {
                var getDistanceRuler = await context.DistanceRulers.SingleOrDefaultAsync(x => x.Id == Id);
                if (getDistanceRuler != null)
                {
                    context.DistanceRulers.Remove(getDistanceRuler);
                    await context.SaveChangesAsync();

                    return ResponseDto<BaseResponseDto>.Success(new() { Id = getDistanceRuler.Id }, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Mesafe Cetveli Bulunamadı..", Description = "Aradığınız Mesafe Cetveli Bulunamadı.." } }, 400);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<DistanceRulerGetResponseDto>> Get(Guid Id)
        {
            try
            {
                var getDistanceRuler = await context.DistanceRulers
                    .Select(distanceRuler => new DistanceRulerGetResponseDto()
                    {
                        Id = distanceRuler.Id,
                        Icon = distanceRuler.Icon,
                        VillaId = distanceRuler.VillaId,
                        HotelId = distanceRuler.HotelId,
                        Value = distanceRuler.Value,
                        DistanceRulerDetails = distanceRuler.DistanceRulerDetails.Select(distanceRulerDetail => new DistanceRulerGetResponseDtoDistanceRulerDetail()
                        {
                            Id = distanceRulerDetail.Id,
                            LanguageCode = distanceRulerDetail.LanguageCode,
                            Name = distanceRulerDetail.Name,

                        }).ToList(),
                        Villa = new DistanceRulerGetResponseDtoVilla()
                        {
                            Id = distanceRuler.Villa.Id,
                            VillaDetails = distanceRuler.Villa.VillaDetails.Select(distanceRulerVillaDetail => new DistanceRulerGetResponseDtoVillaDetail()
                            {
                                Id = distanceRulerVillaDetail.Id,
                                LanguageCode = distanceRulerVillaDetail.LanguageCode,
                                Name = distanceRulerVillaDetail.Name,
                            }).ToList(),
                        },
                        Hotel = new DistanceRulerGetResponseDtoHotel()
                        {
                            Id = distanceRuler.Villa.Id,
                            HotelDetails = distanceRuler.Hotel.HotelDetails.Select(distanceRulerHotelDetail => new DistanceRulerGetResponseDtoHotelDetail()
                            {
                                Id = distanceRulerHotelDetail.Id,
                                LanguageCode = distanceRulerHotelDetail.LanguageCode,
                                Name = distanceRulerHotelDetail.Name,
                            }).ToList(),
                        }
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == Id);

                return ResponseDto<DistanceRulerGetResponseDto>.Success(getDistanceRuler, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<DistanceRulerGetResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<DistanceRulerGetAllResponseDto>>> GetAll(Guid? VillaId, Guid? HotelId)
        {
            try
            {
                var query = context.DistanceRulers
                    .AsQueryable()
                    .Where(x => x.GeneralStatusType == GeneralStatusType.Active)
                    .Select(distanceRuler => new DistanceRulerGetAllResponseDto()
                    {
                        Id = distanceRuler.Id,
                        Icon = distanceRuler.Icon,
                        Value = distanceRuler.Value,
                        DistanceRulerDetails = distanceRuler.DistanceRulerDetails.Select(distanceRulerDetail => new DistanceRulerGetAllResponseDtoDistanceRulerDetail()
                        {
                            Id = distanceRulerDetail.Id,
                            LanguageCode = distanceRulerDetail.LanguageCode,
                            Name = distanceRulerDetail.Name,
                        }).ToList(),
                        VillaId = distanceRuler.VillaId,
                        Villa = new DistanceRulerGetAllResponseDtoVilla()
                        {
                            Id = distanceRuler.Villa.Id,
                            VillaDetails = distanceRuler.Villa.VillaDetails.Select(distanceRulerVillaDetail => new DistanceRulerGetAllResponseDtoVillaDetail()
                            {
                                Id = distanceRulerVillaDetail.Id,
                                LanguageCode = distanceRulerVillaDetail.LanguageCode,
                                Name = distanceRulerVillaDetail.Name,
                            }).ToList(),
                        },
                        HotelId = distanceRuler.HotelId,
                        Hotel = new DistanceRulerGetAllResponseDtoHotel()
                        {
                            Id = distanceRuler.Villa.Id,
                            HotelDetails = distanceRuler.Hotel.HotelDetails.Select(distanceRulerHotelDetail => new DistanceRulerGetAllResponseDtoHotelDetail()
                            {
                                Id = distanceRulerHotelDetail.Id,
                                LanguageCode = distanceRulerHotelDetail.LanguageCode,
                                Name = distanceRulerHotelDetail.Name,
                            }).ToList(),
                        }
                    });

                var getDistanceRulers = await query
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<DistanceRulerGetAllResponseDto>>.Success(getDistanceRulers, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<DistanceRulerGetAllResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> Update(DistanceRulerUpdateRequestDto model, Guid userId)
        {
            try
            {
                var getDistanceRuler = await context.DistanceRulers.SingleOrDefaultAsync(x => x.Id == model.Id);
                if (getDistanceRuler != null)
                {
                    getDistanceRuler.UpdatedAt = DateTime.Now;
                    getDistanceRuler.UpdatedById = userId;

                    if ((getDistanceRuler.Icon != model.Icon) && model.Icon is not null) getDistanceRuler.Icon = model.Icon;
                    if ((getDistanceRuler.GeneralStatusType != model.GeneralStatusType) && model.GeneralStatusType > 0) getDistanceRuler.GeneralStatusType = model.GeneralStatusType;
                    if ((getDistanceRuler.Value != model.Value) && model.Value is not null) getDistanceRuler.Value = model.Value;

                    context.DistanceRulers.Update(getDistanceRuler);
                    var result = await context.SaveChangesAsync();


                    return ResponseDto<BaseResponseDto>.Success(new() { Id = getDistanceRuler.Id }, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Mesafe Cetveli Bulunamadı..", Description = "Aradığınız Mesafe Cetveli Bulunamadı.." } }, 400);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> UpdateDetail(DistanceRulerDetailUpdateRequestDto model, Guid userId)
        {
            try
            {
                var getDistanceRulerDetail = await context.DistanceRulerDetails.SingleOrDefaultAsync(x => x.Id == model.Id);
                if (getDistanceRulerDetail != null)
                {
                    getDistanceRulerDetail.UpdatedAt = DateTime.Now;
                    getDistanceRulerDetail.UpdatedById = userId;

                    if ((getDistanceRulerDetail.Name != model.Name) && model.Name is not null) getDistanceRulerDetail.Name = model.Name;
                    if ((getDistanceRulerDetail.GeneralStatusType != model.GeneralStatusType) && model.GeneralStatusType > 0) getDistanceRulerDetail.GeneralStatusType = model.GeneralStatusType;

                    context.DistanceRulerDetails.Update(getDistanceRulerDetail);
                    var result = await context.SaveChangesAsync();

                    return ResponseDto<BaseResponseDto>.Success(new() { Id = getDistanceRulerDetail.Id }, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Mesafe Cetveli Bulunamadı..", Description = "Aradığınız Mesafe Cetveli Bulunamadı.." } }, 400);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }
    }
}
