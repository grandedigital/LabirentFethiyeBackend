using LabirentFethiye.Application.Abstracts.ProjectInterfaces;
using LabirentFethiye.Common.Dtos.GlobalDtos.CityDtos.CityResponseDtos;
using LabirentFethiye.Common.Dtos.GlobalDtos.DistrictDtos.DistrictResponseDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.HotelDtos.HotelRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.HotelDtos.HotelResponseDtos;
using LabirentFethiye.Common.Dtos.TownDtos.TownResponseDtos;
using LabirentFethiye.Common.Enums;
using LabirentFethiye.Common.Responses;
using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Contexts;
using LabirentFethiye.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;

namespace LabirentFethiye.Persistence.Concrete.ProjectConcretes
{
    public class HotelService : IHotelService
    {
        private readonly AppDbContext context;

        public HotelService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<ResponseDto<BaseResponseDto>> Create(HotelCreateRequestDto model, Guid userId)
        {
            try
            {
                string urlReplace = GeneralFunctions.UrlReplace(model.Name);
                var urlIsAny = context.Hotels.Any(x => x.Slug == urlReplace);
                while (urlIsAny)
                {
                    urlReplace = GeneralFunctions.UrlReplace(urlReplace + "-1");
                    urlIsAny = context.Villas.Any(x => x.Slug == urlReplace);
                }

                Hotel hotel = new()
                {
                    GeneralStatusType = GeneralStatusType.Active,
                    CreatedAt = DateTime.Now,
                    CreatedById = userId,
                    Room = model.Room,
                    Bath = model.Bath,
                    Person = model.Person,
                    GoogleMap = model.GoogleMap,
                    PriceType = model.PriceType,

                    TownId = model.TownId is not null ? Guid.Parse(model.TownId.ToString()) : Guid.Empty,

                    MetaTitle = model.MetaTitle is not null ? model.MetaTitle : model.Name,
                    MetaDescription = model.MetaDescription is not null ? model.MetaDescription : model.Name,
                    Slug = urlReplace,

                    ElectricityMeterNumber = model.ElectricityMeterNumber,
                    InternetMeterNumber = model.InternetMeterNumber,
                    WaterMaterNumber = model.WaterMaterNumber,
                    WifiPassword = model.WifiPassword,
                    Line = 0,

                    HotelDetails = new List<HotelDetail>(){
                        new() {
                            LanguageCode = model.LanguageCode,
                            Name = model.Name,
                            DescriptionShort = model.DescriptionShort,
                            DescriptionLong = model.DescriptionLong,
                            FeatureTextBlue = model.FeatureTextBlue,
                            FeatureTextRed = model.FeatureTextRed,
                            FeatureTextWhite = model.FeatureTextWhite
                        }
                    }
                };

                await context.Hotels.AddAsync(hotel);
                await context.SaveChangesAsync();

                return ResponseDto<BaseResponseDto>.Success(new() { Id = hotel.Id }, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> CreateDetail(HotelDetailCreateRequestDto model, Guid userId)
        {
            try
            {
                HotelDetail hotelDetail = new()
                {
                    HotelId = model.HotelId,
                    LanguageCode = model.LanguageCode,
                    Name = model.Name,
                    DescriptionShort = model.DescriptionShort,
                    DescriptionLong = model.DescriptionLong,
                    FeatureTextBlue = model.FeatureTextBlue,
                    FeatureTextRed = model.FeatureTextRed,
                    FeatureTextWhite = model.FeatureTextWhite,
                    CreatedById = userId,
                };

                await context.HotelDetails.AddAsync(hotelDetail);
                await context.SaveChangesAsync();

                return ResponseDto<BaseResponseDto>.Success(new() { Id = hotelDetail.Id }, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<HotelGetResponseDto>> Get(Guid Id)
        {
            try
            {
                var getHotel = await context.Hotels
                   .Select(hotel => new HotelGetResponseDto()
                   {
                       Bath = hotel.Bath,
                       Room = hotel.Room,
                       Person = hotel.Person,
                       ElectricityMeterNumber = hotel.ElectricityMeterNumber,
                       GeneralStatusType = hotel.GeneralStatusType,
                       GoogleMap = hotel.GoogleMap,
                       Id = hotel.Id,
                       InternetMeterNumber = hotel.InternetMeterNumber,
                       MetaDescription = hotel.MetaDescription,
                       MetaTitle = hotel.MetaTitle,
                       PriceType = hotel.PriceType,
                       Slug = hotel.Slug,
                       TownId = hotel.TownId,
                       WaterMaterNumber = hotel.WaterMaterNumber,
                       WifiPassword = hotel.WifiPassword,
                       HotelDetails = hotel.HotelDetails.Select(hotelDetail => new HotelGetResponseDtoHotelDetail()
                       {
                           DescriptionLong = hotelDetail.DescriptionLong,
                           DescriptionShort = hotelDetail.DescriptionShort,
                           FeatureTextBlue = hotelDetail.FeatureTextBlue,
                           FeatureTextRed = hotelDetail.FeatureTextRed,
                           FeatureTextWhite = hotelDetail.FeatureTextWhite,
                           GeneralStatusType = hotelDetail.GeneralStatusType,
                           Id = hotelDetail.Id,
                           LanguageCode = hotelDetail.LanguageCode,
                           Name = hotelDetail.Name
                       }).ToList(),
                       Photos = hotel.Photos.OrderBy(x => x.Line).Select(photo => new HotelGetResponseDtoPhoto()
                       {
                           Image = photo.Image,
                           ImgAlt = photo.ImgAlt,
                           ImgTitle = photo.ImgTitle,
                           Line = photo.Line,
                           Title = photo.Title,
                           VideoLink = photo.VideoLink
                       }).ToList(),
                       Payments = hotel.Payments.Select(payment => new HotelGetResponseDtoPayments()
                       {
                           Amount = payment.Amount,
                           InOrOut = payment.InOrOut
                       }).ToList(),
                   })
                   .AsNoTracking()
                   .FirstOrDefaultAsync(x => x.Id == Id);

                return ResponseDto<HotelGetResponseDto>.Success(getHotel, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<HotelGetResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<HotelGetAllResponseDto>>> GetAll(HotelGetAllRequestDto model)
        {
            try
            {
                var query = context.Hotels
                    .AsQueryable()
                    .Where(x => x.GeneralStatusType == GeneralStatusType.Active);

                if (model.SearchName is not null)
                    query = query.Where(x => x.HotelDetails.Any(x => x.Name.ToLower().Contains(model.SearchName.ToLower())));

                if (model.OrderByName is not null)
                    if (model.OrderByName == true) query = query.OrderBy(x => x.HotelDetails.FirstOrDefault().Name);
                    else query = query.OrderByDescending(x => x.HotelDetails.FirstOrDefault().Name);
                else
                    query = query.OrderByDescending(x => x.CreatedAt);

                PageInfo pageInfo = GeneralFunctions.PageInfoHelper(Page: model.Pagination.Page, Size: model.Pagination.Size, TotalCount: await query.CountAsync());

                var getAllHotel = await query
                    .Select(villa => new HotelGetAllResponseDto()
                    {
                        Id = villa.Id,
                        Bath = villa.Bath,
                        Room = villa.Room,
                        Person = villa.Person,
                        GoogleMap = villa.GoogleMap,
                        GeneralStatusType = villa.GeneralStatusType,
                        TownId = villa.TownId,
                        Town = new TownGetResponseDto()
                        {
                            Id = villa.Town.Id,
                            Name = villa.Town.Name,
                            DistrictId = villa.Town.DistrictId,
                            District = new DistrictGetResponseDto()
                            {
                                Name = villa.Town.District.Name,
                                CityId = villa.Town.District.City.Id,
                                City = new CityGetResponseDto()
                                {
                                    Name = villa.Town.District.City.Name,
                                    CityNumber = villa.Town.District.City.CityNumber,
                                }
                            }
                        },
                        HotelDetails = villa.HotelDetails.Select(villaDetail => new HotelGetAllResponseDtoHotelDetail()
                        {
                            Id = villaDetail.Id,
                            Name = villaDetail.Name,
                            DescriptionLong = villaDetail.DescriptionLong,
                            DescriptionShort = villaDetail.DescriptionShort,
                            FeatureTextBlue = villaDetail.FeatureTextBlue,
                            FeatureTextRed = villaDetail.FeatureTextRed,
                            FeatureTextWhite = villaDetail.FeatureTextWhite,
                            GeneralStatusType = villaDetail.GeneralStatusType,
                            LanguageCode = villaDetail.LanguageCode
                        }).ToList()
                    })
                    .Skip(model.Pagination.Page * model.Pagination.Size)
                    .Take(model.Pagination.Size)
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<HotelGetAllResponseDto>>.Success(getAllHotel, 200, pageInfo);

                //var getAllHotel = context.Hotels
                //    .AsQueryable()
                //    .Where(x => x.GeneralStatusType == GeneralStatusType.Active)
                //    .Include(x => x.HotelDetails)
                //    .Select(villa => new HotelGetAllResponseDto()
                //    {
                //        Id = villa.Id,
                //        Bath = villa.Bath,
                //        Room = villa.Room,
                //        Person = villa.Person,
                //        GoogleMap = villa.GoogleMap,
                //        GeneralStatusType = villa.GeneralStatusType,
                //        TownId = villa.TownId,
                //        Town = new TownGetResponseDto()
                //        {
                //            Id = villa.Town.Id,
                //            Name = villa.Town.Name,
                //            DistrictId = villa.Town.DistrictId,
                //            District = new DistrictGetResponseDto()
                //            {
                //                Name = villa.Town.District.Name,
                //                CityId = villa.Town.District.City.Id,
                //                City = new CityGetResponseDto()
                //                {
                //                    Name = villa.Town.District.City.Name,
                //                    CityNumber = villa.Town.District.City.CityNumber,
                //                }
                //            }
                //        },
                //        HotelDetails = villa.HotelDetails.Select(villaDetail => new HotelGetAllResponseDtoHotelDetail()
                //        {
                //            Id = villaDetail.Id,
                //            Name = villaDetail.Name,
                //            DescriptionLong = villaDetail.DescriptionLong,
                //            DescriptionShort = villaDetail.DescriptionShort,
                //            FeatureTextBlue = villaDetail.FeatureTextBlue,
                //            FeatureTextRed = villaDetail.FeatureTextRed,
                //            FeatureTextWhite = villaDetail.FeatureTextWhite,
                //            GeneralStatusType = villaDetail.GeneralStatusType,
                //            LanguageCode = villaDetail.LanguageCode
                //        }).ToList()
                //    })
                //    .Skip(model.Pagination.Page * model.Pagination.Size)
                //    .Take(model.Pagination.Size);

                //if (model.SearchName is not null)
                //    getAllHotel = getAllHotel.Where(x => x.HotelDetails.Any(x => x.Name.Contains(model.SearchName)));

                //if (model.OrderByName is not null)
                //    if (model.OrderByName == true) { getAllHotel = getAllHotel.OrderBy(x => x.HotelDetails.FirstOrDefault().Name); }
                //    else { getAllHotel = getAllHotel.OrderByDescending(x => x.HotelDetails.FirstOrDefault().Name); }
                //else
                //    getAllHotel = getAllHotel.OrderByDescending(x => x.CreatedAt);

                //PageInfo pageInfo = GeneralFunctions.PageInfoHelper(Page: model.Pagination.Page, Size: model.Pagination.Size, TotalCount: await context.Hotels.Where(x => x.GeneralStatusType == GeneralStatusType.Active).CountAsync());

                //return ResponseDto<ICollection<HotelGetAllResponseDto>>.Success(await getAllHotel.AsNoTracking().ToListAsync(), 200, pageInfo);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<HotelGetAllResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> Update(HotelUpdateRequestDto model, Guid userId)
        {
            try
            {
                var getHotel = await context.Hotels.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (getHotel != null)
                {

                    if (!String.IsNullOrEmpty(model.Slug))
                    {
                        string urlReplace = GeneralFunctions.UrlReplace(model.Slug);
                        if (getHotel.Slug != urlReplace)
                        {
                            var urlIsAny = context.Hotels.Any(x => x.Id != getHotel.Id && x.Slug == urlReplace);
                            if (urlIsAny) return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Update", Description = "Yazdığınız Url başka bir villa tarafından kullanılıyor. Lütfen başka bir url yazınız.." } }, 400);
                            getHotel.Slug = urlReplace;
                        }
                    }

                    getHotel.UpdatedAt = DateTime.Now;
                    getHotel.UpdatedById = userId;

                    if ((getHotel.GoogleMap != model.GoogleMap) && model.GoogleMap is not null) getHotel.GoogleMap = model.GoogleMap;
                    if ((getHotel.MetaTitle != model.MetaTitle) && model.MetaTitle is not null) getHotel.MetaTitle = model.MetaTitle;
                    if ((getHotel.MetaDescription != model.MetaDescription) && model.MetaDescription is not null) getHotel.MetaDescription = model.MetaDescription;

                    if ((getHotel.Room != model.Room) && model.Room > 0) getHotel.Room = model.Room;
                    if ((getHotel.Bath != model.Bath) && model.Bath > 0) getHotel.Bath = model.Bath;
                    if ((getHotel.Person != model.Person) && model.Person > 0) getHotel.Person = model.Person;

                    if ((getHotel.GeneralStatusType != model.GeneralStatusType) && model.GeneralStatusType > 0) getHotel.GeneralStatusType = model.GeneralStatusType;
                    if ((getHotel.PriceType != model.PriceType) && model.PriceType > 0) getHotel.PriceType = model.PriceType;

                    if ((getHotel.ElectricityMeterNumber != model.ElectricityMeterNumber) && model.ElectricityMeterNumber is not null) getHotel.ElectricityMeterNumber = model.ElectricityMeterNumber;
                    if ((getHotel.InternetMeterNumber != model.InternetMeterNumber) && model.InternetMeterNumber is not null) getHotel.InternetMeterNumber = model.InternetMeterNumber;
                    if ((getHotel.WaterMaterNumber != model.WaterMaterNumber) && model.WaterMaterNumber is not null) getHotel.WaterMaterNumber = model.WaterMaterNumber;
                    if ((getHotel.WifiPassword != model.WifiPassword) && model.WifiPassword is not null) getHotel.WifiPassword = model.WifiPassword;

                    if ((getHotel.Line != model.Line) && model.Line > 0) getHotel.Line = model.Line;

                    context.Hotels.Update(getHotel);
                    var result = await context.SaveChangesAsync();

                    return ResponseDto<BaseResponseDto>.Success(new() { Id = getHotel.Id }, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Update", Description = "Otel Bulunamadı.." } }, 400);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> UpdateDetail(HotelDetailUpdateRequestDto model, Guid userId)
        {
            try
            {
                var getHotelDetail = await context.HotelDetails.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (getHotelDetail != null)
                {
                    getHotelDetail.UpdatedAt = DateTime.Now;
                    getHotelDetail.UpdatedById = userId;

                    if ((getHotelDetail.Name != model.Name) && model.Name is not null) getHotelDetail.Name = model.Name;
                    if ((getHotelDetail.DescriptionShort != model.DescriptionShort) && model.DescriptionShort is not null) getHotelDetail.DescriptionShort = model.DescriptionShort;
                    if ((getHotelDetail.DescriptionLong != model.DescriptionLong) && model.DescriptionLong is not null) getHotelDetail.DescriptionLong = model.DescriptionLong;
                    if ((getHotelDetail.GeneralStatusType != model.GeneralStatusType) && model.GeneralStatusType > 0) getHotelDetail.GeneralStatusType = model.GeneralStatusType;

                    if ((getHotelDetail.FeatureTextWhite != model.FeatureTextWhite) && model.FeatureTextWhite is not null) getHotelDetail.FeatureTextWhite = model.FeatureTextWhite;
                    if ((getHotelDetail.FeatureTextRed != model.FeatureTextRed) && model.FeatureTextRed is not null) getHotelDetail.FeatureTextRed = model.FeatureTextRed;
                    if ((getHotelDetail.FeatureTextBlue != model.FeatureTextBlue) && model.FeatureTextBlue is not null) getHotelDetail.FeatureTextBlue = model.FeatureTextBlue;

                    context.HotelDetails.Update(getHotelDetail);
                    var result = await context.SaveChangesAsync();

                    return ResponseDto<BaseResponseDto>.Success(new() { Id = getHotelDetail.Id }, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Update", Description = "Otel Bulunamadı.." } }, 400);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }
    }
}
