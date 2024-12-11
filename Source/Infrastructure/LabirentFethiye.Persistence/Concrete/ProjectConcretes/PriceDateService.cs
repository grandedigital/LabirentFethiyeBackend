using LabirentFethiye.Application.Abstracts.ProjectInterfaces;
using LabirentFethiye.Common.Dtos.ProjectDtos.PriceDateDtos.PriceDateRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.PriceDateDtos.PriceDateResponseDtos;
using LabirentFethiye.Common.Enums;
using LabirentFethiye.Common.Responses;
using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LabirentFethiye.Persistence.Concrete.ProjectConcretes
{
    public class PriceDateService : IPriceDateService
    {
        private readonly AppDbContext context;

        public PriceDateService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<ResponseDto<BaseResponseDto>> Create(PriceDateCreateRequestDto model, Guid userId)
        {
            try
            {
                if (model.StartDate > model.EndDate)
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Create Errors..", Description = "CheckIn Tarihi CheckOut tarihinden büyük olamaz.." } }, 400);
                if (model.StartDate == model.EndDate)
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Create Errors..", Description = "CheckIn Tarihi CheckOut tarihine eşit olamaz.." } }, 400);
                if (model.VillaId == null && model.RoomId == null)
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Create Errors..", Description = "Tesis Id boş olamaz" } }, 400);
                //------

                var prices = await GetPriceForDate(new() { VillaId = model.VillaId, RoomId = model.RoomId, CheckIn = model.StartDate, CheckOut = model.EndDate });
                if (prices.Data == null)
                {
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "GetPriceForDate Errors..", Description = $"Başlangıç Tarihi : {model.StartDate}, Bitiş Tarihi : {model.EndDate} olan tarihler fiyat içeriyor. Lütfen Bu tarihleri kapsayan başka bir fiyat verisi olmadığına emin olun.." } }, 400);
                }
                //-----

                PriceDate priceDate = new()
                {
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Price = model.Price,
                    VillaId = model.VillaId,
                    RoomId = model.RoomId,
                    GeneralStatusType = GeneralStatusType.Active,
                    CreatedAt = DateTime.Now,
                    CreatedById = userId
                };

                await context.PriceDates.AddAsync(priceDate);
                await context.SaveChangesAsync();

                return ResponseDto<BaseResponseDto>.Success(new() { Id = priceDate.Id }, 200);

            }
            catch (Exception ex) { return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500); }
        }

        public async Task<ResponseDto<BaseResponseDto>> DeleteHard(Guid Id)
        {
            try
            {
                var getPriceDate = await context.PriceDates.FirstOrDefaultAsync(x => x.Id == Id);
                if (getPriceDate != null)
                {
                    context.PriceDates.Remove(getPriceDate);
                    await context.SaveChangesAsync();
                    return ResponseDto<BaseResponseDto>.Success(new() { Id = getPriceDate.Id }, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Delete Errors..", Description = "Fiyat Bulunamadı.." } }, 400);
            }
            catch (Exception ex) { return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500); }
        }

        public async Task<ResponseDto<PriceDateGetResponseDto>> Get(Guid Id)
        {
            try
            {
                var getPriceDate = await context.PriceDates
                    .Where(x => x.Id == Id)
                    .Select(priceDate => new PriceDateGetResponseDto()
                    {
                        Id = priceDate.Id,
                        EndDate = priceDate.EndDate,
                        StartDate = priceDate.StartDate,
                        Price = priceDate.Price,
                        RoomId = priceDate.RoomId,
                        VillaId = priceDate.VillaId
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                return ResponseDto<PriceDateGetResponseDto>.Success(getPriceDate, 200);

            }
            catch (Exception ex) { return ResponseDto<PriceDateGetResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500); }
        }

        public async Task<ResponseDto<ICollection<PriceDateGetAllResponseDto>>> GetAll(Guid? VillaId, Guid? RoomId)
        {
            try
            {
                var query = context.PriceDates.AsQueryable();

                if (VillaId != null)
                    query = query.Where(x => x.VillaId == VillaId);
                else if (RoomId != null)
                    query = query.Where(x => x.RoomId == RoomId);

                query = query.Where(x => x.GeneralStatusType == GeneralStatusType.Active);

                var response = await query
                    .Select(priceDate => new PriceDateGetAllResponseDto()
                    {
                        Id = priceDate.Id,
                        EndDate = priceDate.EndDate,
                        StartDate = priceDate.StartDate,
                        Price = priceDate.Price,
                        RoomId = priceDate.RoomId,
                        VillaId = priceDate.VillaId,
                        Villa = (priceDate.Villa != null ? new PriceDateGetAllResponseDtoVilla() { PriceType = priceDate.Villa.PriceType } : null),
                        Room = (priceDate.Room != null ? new PriceDateGetAllResponseDtoRoom() { PriceType = priceDate.Room.Hotel.PriceType } : null)
                    })
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<PriceDateGetAllResponseDto>>.Success(response, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<PriceDateGetAllResponseDto>>.Fail(new() { new () { Title = "Exception Errors..", Description = ex.Message.ToString()
    }
}, 500);
            }
        }

        public async Task<ResponseDto<ICollection<PriceDateGetForDateResponseDto>>> GetPriceForDate(PriceDateGetForDateRequestDto model)
        {
            try
            {

                if (model.CheckIn.Date > model.CheckOut.Date)
                    return ResponseDto<ICollection<PriceDateGetForDateResponseDto>>.Fail(new() { new() { Title = "GetReservationPrice Errors..", Description = "CheckIn Tarihi CheckOut tarihinden büyük olamaz.." } }, 400);
                if (model.CheckIn.Date == model.CheckOut.Date)
                    return ResponseDto<ICollection<PriceDateGetForDateResponseDto>>.Fail(new() { new() { Title = "GetReservationPrice Errors..", Description = "CheckIn Tarihi CheckOut tarihine eşit olamaz.." } }, 400);
                if (model.VillaId == Guid.Empty || model.RoomId == Guid.Empty)
                    return ResponseDto<ICollection<PriceDateGetForDateResponseDto>>.Fail(new() { new() { Title = "GetReservationPrice Errors..", Description = "Tesis Id boş olamaz" } }, 400);

                //-----

                model.CheckOut = model.CheckOut.AddDays(-1);

                var query = context.PriceDates
                    .AsQueryable()
                    .Where(x => x.GeneralStatusType == GeneralStatusType.Active);

                if (model.Id != null)
                    query = query.Where(x => x.Id != model.Id);

                if (model.VillaId != null)
                    query = query.Where(x => (x.VillaId == model.VillaId) && ((model.CheckIn < x.StartDate && model.CheckOut >= x.StartDate) || (model.CheckIn >= x.StartDate && model.CheckIn <= x.EndDate) || (model.CheckOut >= x.StartDate && model.CheckOut <= x.EndDate)));

                else if (model.RoomId != null)
                    query = query.Where(x => (x.RoomId == model.RoomId) && ((model.CheckIn < x.StartDate && model.CheckOut >= x.StartDate) || (model.CheckIn >= x.StartDate && model.CheckIn <= x.EndDate) || (model.CheckOut >= x.StartDate && model.CheckOut <= x.EndDate)));

                List<PriceDate> priceDates = await query
                    .OrderBy(x => x.StartDate)
                    .AsNoTracking()
                    .ToListAsync();

                if (priceDates.Any(x => model.CheckIn >= x.StartDate && model.CheckIn <= x.EndDate) && priceDates.Any(x => model.CheckOut >= x.StartDate && model.CheckOut <= x.EndDate))
                {
                    List<PriceDateGetForDateResponseDto> response = new List<PriceDateGetForDateResponseDto>();

                    DateTime fakeDay = model.CheckIn;
                    foreach (var price in priceDates)
                    {
                        while (fakeDay >= price.StartDate && fakeDay <= price.EndDate)
                        {
                            if (fakeDay > model.CheckOut) break;
                            response.Add(new() { Date = fakeDay, Price = price.Price, PriceType = price.Villa != null ? price.Villa.PriceType : PriceType.TL });
                            fakeDay = fakeDay.AddDays(1);
                        }
                    }

                    return ResponseDto<ICollection<PriceDateGetForDateResponseDto>>.Success(response, 200);

                }
                else
                    return ResponseDto<ICollection<PriceDateGetForDateResponseDto>>.Fail(new() { new() { Title = "GetReservationPrice Errors..", Description = "Tesise Ait İlgili Tarihlerin Bir Kısmı İçin Fiyat Bulunamadı.." } }, 400);

                //var aa = priceDates.Any(x => model.CheckIn >= x.StartDate && model.CheckIn <= x.EndDate);


                //List<PriceDate> priceDates = new();

                //if (model.VillaId != null)
                //{
                //    priceDates = await context.PriceDates
                //    .Where(x => (x.VillaId == model.VillaId && x.GeneralStatusType == GeneralStatusType.Active) && ((model.CheckIn < x.StartDate && model.CheckOut >= x.StartDate) || (model.CheckIn >= x.StartDate && model.CheckIn <= x.EndDate) || (model.CheckOut >= x.StartDate && model.CheckOut <= x.EndDate)))
                //    .Include(x => x.Villa)
                //    .OrderBy(x => x.StartDate)
                //    .AsNoTracking()
                //    .ToListAsync();
                //}
                //else if (model.RoomId != null)
                //{
                //    priceDates = await context.PriceDates
                //    .Where(x => (x.RoomId == model.RoomId && x.GeneralStatusType == GeneralStatusType.Active) && ((model.CheckIn < x.StartDate && model.CheckOut >= x.StartDate) || (model.CheckIn >= x.StartDate && model.CheckIn <= x.EndDate) || (model.CheckOut >= x.StartDate && model.CheckOut <= x.EndDate)))
                //    .Include(x => x.Room)
                //    .OrderBy(x => x.StartDate)
                //    .AsNoTracking()
                //    .ToListAsync();
                //}

                //var response = new List<PriceDateGetForDateResponseDto>();

                //DateTime fakeDay = model.CheckIn;
                //foreach (var price in priceDates)
                //{
                //    while (fakeDay >= price.StartDate && fakeDay <= price.EndDate)
                //    {
                //        if (fakeDay > model.CheckOut) break;
                //        response.Add(new() { Date = fakeDay, Price = price.Price, PriceType = price.Villa != null ? price.Villa.PriceType : PriceType.TL });
                //        fakeDay = fakeDay.AddDays(1);
                //    }
                //}

                //return ResponseDto<ICollection<PriceDateGetForDateResponseDto>>.Success(response, 200);
            }
            catch (Exception ex) { return ResponseDto<ICollection<PriceDateGetForDateResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500); }
        }

        public async Task<ResponseDto<BaseResponseDto>> Update(PriceDateUpdateRequestDto model, Guid userId)
        {
            try
            {
                if (model.StartDate > DateTime.MinValue || model.EndDate > DateTime.MinValue)
                {
                    if (model.StartDate < DateTime.MinValue || model.EndDate < DateTime.MinValue)
                        return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Update Errors..", Description = "Tarihleri Kontrol Ediniz.." } }, 400);

                    if (model.StartDate > model.EndDate)
                        return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Update Errors..", Description = "Fiyat Başlangıç Tarihi Fiyat Bitiş tarihinden büyük olamaz.." } }, 400);
                    if (model.StartDate == model.EndDate)
                        return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Update Errors..", Description = "Fiyat Başlangıç Tarihi Fiyat Bitiş tarihine eşit olamaz.." } }, 400);
                }
                //------

                var getPriceDate = await context.PriceDates.FirstOrDefaultAsync(x => x.Id == model.Id);

                if (model.StartDate > DateTime.MinValue || model.EndDate > DateTime.MinValue)
                {
                    var prices = await GetPriceForDate(new() { VillaId = getPriceDate.VillaId, RoomId = getPriceDate.RoomId, CheckIn = model.StartDate, CheckOut = model.EndDate, Id = model.Id });
                    if (prices.Data.Count > 0)
                    {
                        return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "GetPriceForDate Errors..", Description = $"Başlangıç Tarihi : {model.StartDate}, Bitiş Tarihi : {model.EndDate} olan tarihler fiyat içeriyor. Lütfen Bu tarihleri kapsayan başka bir fiyat verisi olmadığına emin olun.." } }, 400);
                    }
                }
                //-----


                if (getPriceDate != null)
                {
                    getPriceDate.UpdatedAt = DateTime.Now;
                    getPriceDate.UpdatedById = userId;

                    if ((getPriceDate.Price != model.Price) && model.Price > 0) getPriceDate.Price = model.Price;
                    if (getPriceDate.StartDate != model.StartDate && model.StartDate > DateTime.MinValue) getPriceDate.StartDate = model.StartDate;
                    if (getPriceDate.EndDate != model.EndDate && model.EndDate > DateTime.MinValue) getPriceDate.EndDate = model.EndDate;

                    if ((getPriceDate.GeneralStatusType != model.GeneralStatusType) && model.GeneralStatusType > 0) getPriceDate.GeneralStatusType = model.GeneralStatusType;

                    context.PriceDates.Update(getPriceDate);
                    await context.SaveChangesAsync();

                    return ResponseDto<BaseResponseDto>.Success(new() { Id = getPriceDate.Id }, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Update Errors..", Description = "Fiyat Bilgisi Bulunamadı.." } }, 400);
            }
            catch (Exception ex) { return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500); }
        }
    }
}
