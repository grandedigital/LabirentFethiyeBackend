using LabirentFethiye.Application.Abstracts.ProjectInterfaces;
using LabirentFethiye.Common.Dtos.ProjectDtos.RoomDtos.RoomRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.RoomDtos.RoomResponseDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.VillaDtos.VillaResponseDtos;
using LabirentFethiye.Common.Enums;
using LabirentFethiye.Common.Responses;
using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Contexts;
using LabirentFethiye.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;

namespace LabirentFethiye.Persistence.Concrete.ProjectConcretes
{
    public class RoomService : IRoomService
    {
        private readonly AppDbContext context;

        public RoomService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<ResponseDto<BaseResponseDto>> Create(RoomCreateRequestDto model, Guid userId)
        {
            try
            {
                string urlReplace = GeneralFunctions.UrlReplace(model.Name);
                var urlIsAny = context.Rooms.Any(x => x.Slug == urlReplace);
                while (urlIsAny)
                {
                    urlReplace = GeneralFunctions.UrlReplace(urlReplace + "-1");
                    urlIsAny = context.Rooms.Any(x => x.Slug == urlReplace);
                }

                Room room = new()
                {
                    HotelId = model.HotelId,
                    GeneralStatusType = Common.Enums.GeneralStatusType.Active,
                    CreatedAt = DateTime.Now,
                    CreatedById = userId,
                    Rooms = model.Rooms,
                    Bath = model.Bath,
                    Person = model.Person,
                    MetaTitle = model.MetaTitle is not null ? model.MetaTitle : model.Name,
                    MetaDescription = model.MetaDescription is not null ? model.MetaDescription : model.Name,
                    Slug = urlReplace,
                    OnlineReservation = model.OnlineReservation,
                    PriceType = model.PriceType,
                    ElectricityMeterNumber = model.ElectricityMeterNumber,
                    InternetMeterNumber = model.InternetMeterNumber,
                    WaterMaterNumber = model.WaterMaterNumber,
                    WifiPassword = model.WifiPassword,
                    Line = 0,
                    RoomDetails = new List<RoomDetail>()
                    {
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

                await context.Rooms.AddAsync(room);
                await context.SaveChangesAsync();

                return ResponseDto<BaseResponseDto>.Success(new() { Id = room.Id }, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> CreateDetail(RoomDetailCreateRequestDto model, Guid userId)
        {
            try
            {
                RoomDetail roomDetail = new()
                {
                    RoomId = model.RoomId,
                    LanguageCode = model.LanguageCode,
                    Name = model.Name,
                    DescriptionShort = model.DescriptionShort,
                    DescriptionLong = model.DescriptionLong,
                    FeatureTextBlue = model.FeatureTextBlue,
                    FeatureTextRed = model.FeatureTextRed,
                    FeatureTextWhite = model.FeatureTextWhite,
                    CreatedById = userId,
                };

                await context.RoomDetails.AddAsync(roomDetail);
                var result = await context.SaveChangesAsync();

                return ResponseDto<BaseResponseDto>.Success(new() { Id = roomDetail.Id }, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<RoomGetResponseDto>> Get(Guid Id)
        {
            try
            {
                var getRoom = await context.Rooms
                    .Include(x => x.RoomDetails)
                    .Include(x => x.Payments)
                    .Include(x => x.Photos.OrderBy(x => x.Line))
                    .Include(x => x.Hotel).ThenInclude(x => x.HotelDetails)
                    .Where(x => x.Id == Id)
                    .Select(room => new RoomGetResponseDto()
                    {
                        Id = room.Id,
                        Person = room.Person,
                        Rooms = room.Rooms,
                        GeneralStatusType = room.GeneralStatusType,
                        Bath = room.Bath,
                        ElectricityMeterNumber = room.ElectricityMeterNumber,
                        InternetMeterNumber = room.InternetMeterNumber,
                        MetaTitle = room.MetaTitle,
                        MetaDescription = room.MetaDescription,
                        OnlineReservation = room.OnlineReservation,
                        PriceType = room.PriceType,
                        Slug = room.Slug,
                        WaterMaterNumber = room.WaterMaterNumber,
                        WifiPassword = room.WifiPassword,
                        RoomDetails = room.RoomDetails.Select(roomDetail => new RoomGetResponseDtoRoomDetail()
                        {
                            Id = roomDetail.Id,
                            LanguageCode = roomDetail.LanguageCode,
                            GeneralStatusType = roomDetail.GeneralStatusType,
                            DescriptionLong = roomDetail.DescriptionLong,
                            DescriptionShort = roomDetail.DescriptionShort,
                            FeatureTextBlue = roomDetail.FeatureTextBlue,
                            FeatureTextRed = roomDetail.FeatureTextRed,
                            FeatureTextWhite = roomDetail.FeatureTextWhite,
                            Name = roomDetail.Name
                        }).ToList(),
                        Hotel = new RoomGetResponseDtoHotel()
                        {
                            Id = room.Hotel.Id,
                            PriceType = room.Hotel.PriceType,
                            HotelDetails = room.Hotel.HotelDetails.Select(hotelDetail => new RoomGetResponseDtoHotelDetail()
                            {
                                Id = hotelDetail.Id,
                                DescriptionLong = hotelDetail.DescriptionLong,
                                DescriptionShort = hotelDetail.DescriptionShort,
                                GeneralStatusType = hotelDetail.GeneralStatusType,
                                LanguageCode = hotelDetail.LanguageCode,
                                Name = hotelDetail.Name
                            }).ToList()
                        },
                        Payments = room.Payments.Select(payment => new RoomGetResponseDtoPayments()
                        {
                            Amount = payment.Amount,
                            InOrOut = payment.InOrOut
                        }).ToList(),
                        Photos = room.Photos.Select(photo => new RoomGetResponseDtoPhotos()
                        {
                            Image = photo.Image,
                            ImgAlt = photo.ImgAlt,
                            ImgTitle = photo.ImgTitle,
                            Line = photo.Line,
                            Title = photo.Title,
                            VideoLink = photo.VideoLink
                        }).ToList()
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                return ResponseDto<RoomGetResponseDto>.Success(getRoom, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<RoomGetResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<RoomGetAllResponseDto>>> GetAll(Guid HotelId)
        {
            try
            {
                var getAllRoom = await context.Rooms
                    .Where(x => x.HotelId == HotelId)
                    .Include(x => x.RoomDetails)
                    .Include(x => x.Hotel).ThenInclude(x => x.HotelDetails)
                    .OrderBy(x => x.Line)
                    .Select(room => new RoomGetAllResponseDto()
                    {
                        Id = room.Id,
                        Person = room.Person,
                        Rooms = room.Rooms,
                        GeneralStatusType = room.GeneralStatusType,
                        Bath = room.Bath,
                        OnlineReservation = room.OnlineReservation,
                        RoomDetails = room.RoomDetails.Select(roomDetail => new RoomGetAllResponseDtoRoomDetail()
                        {
                            Id = roomDetail.Id,
                            LanguageCode = roomDetail.LanguageCode,
                            GeneralStatusType = roomDetail.GeneralStatusType,
                            DescriptionLong = roomDetail.DescriptionLong,
                            DescriptionShort = roomDetail.DescriptionShort,
                            FeatureTextBlue = roomDetail.FeatureTextBlue,
                            FeatureTextRed = roomDetail.FeatureTextRed,
                            FeatureTextWhite = roomDetail.FeatureTextWhite,
                            Name = roomDetail.Name
                        }).ToList(),
                        Hotel = new RoomGetAllResponseDtoHotel()
                        {
                            Id = room.Hotel.Id,
                            HotelDetails = room.Hotel.HotelDetails.Select(hotelDetail => new RoomGetAllResponseDtoHotelDetail()
                            {
                                Id = hotelDetail.Id,
                                DescriptionLong = hotelDetail.DescriptionLong,
                                DescriptionShort = hotelDetail.DescriptionShort,
                                GeneralStatusType = hotelDetail.GeneralStatusType,
                                LanguageCode = hotelDetail.LanguageCode,
                                Name = hotelDetail.Name
                            }).ToList()
                        }
                    })
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<RoomGetAllResponseDto>>.Success(getAllRoom, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<RoomGetAllResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<RoomGetAllAvailableDateResponseDto>>> GetRoomAvailableDates(Guid RoomId)
        {
            try
            {
                List<RoomGetAllAvailableDateResponseDto> responseModel = new();

                // Todo: Bu servis yapılacak


                var reservations = await context.Reservations
                    .AsNoTracking()
                    .Where(x =>
                        (x.RoomId == RoomId && x.ReservationStatusType != ReservationStatusType.Cancaled) &&
                        (x.CheckOut.Date >= DateTime.Now.Date || x.CheckIn.Date == DateTime.Now.Date) &&
                        (x.CheckIn.Year >= DateTime.Now.Year && x.CheckOut.Year >= DateTime.Now.Year)
                    )
                    .OrderBy(x => x.CheckIn)
                    .ToListAsync();





                if (reservations == null || reservations.Count == 0)
                {
                    // Full Avalible
                    //responseModel.Add(new()
                    //{
                    //    StartDate = DateTime.Now.Date.ToShortDateString(),
                    //    EndDate = "31.12." + DateTime.Now.Year.ToString(),
                    //    NightCount = ((new DateTime(DateTime.Now.Year, 12, 31).Date) - DateTime.Now.Date).Days.ToString(),
                    //    Price = ""
                    //});
                }
                else
                {
                    for (int i = 0; i < reservations.Count; i++)
                    {
                        if (i < reservations.Count - 1)
                        {
                            if (reservations[i].CheckIn.Date > DateTime.Now && i == 0)
                            {
                                responseModel.Add(new()
                                {
                                    StartDate = DateTime.Now.Date.ToShortDateString(),
                                    EndDate = reservations[i].CheckIn.Date.ToShortDateString(),
                                    NightCount = (reservations[i].CheckIn.Date - DateTime.Now.Date).Days.ToString(),
                                    Price = ""
                                });
                            }
                            if ((reservations[i + 1].CheckIn.Date - reservations[i].CheckOut.Date).Days > 0)
                            {
                                responseModel.Add(new()
                                {
                                    StartDate = reservations[i].CheckOut.Date.ToShortDateString(),
                                    EndDate = reservations[i + 1].CheckIn.Date.ToShortDateString(),
                                    NightCount = (reservations[i + 1].CheckIn.Date - reservations[i].CheckOut.Date).Days.ToString(),
                                    Price = ""
                                });
                            }
                        }
                        else
                        {
                            responseModel.Add(new()
                            {
                                StartDate = reservations[i].CheckOut.Date.ToShortDateString(),
                                EndDate = "31.12." + reservations[i].CheckOut.Year.ToString(),
                                NightCount = ((new DateTime(reservations[i].CheckOut.Year, 12, 31).Date) - reservations[i].CheckOut.Date).Days.ToString(),
                                Price = ""
                            });
                        }
                    }
                }

                return ResponseDto<ICollection<RoomGetAllAvailableDateResponseDto>>.Success(responseModel, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<RoomGetAllAvailableDateResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> Update(RoomUpdateRequestDto model, Guid userId)
        {
            try
            {
                var getRoom = await context.Rooms.Include(x => x.Hotel).FirstOrDefaultAsync(x => x.Id == model.Id);
                if (getRoom != null)
                {
                    if (!String.IsNullOrEmpty(model.Slug))
                    {
                        string urlReplace = GeneralFunctions.UrlReplace(model.Slug);
                        if (getRoom.Slug != urlReplace)
                        {
                            var urlIsAny = context.Rooms.Any(x => x.Id != getRoom.Id && x.Slug == urlReplace);
                            if (urlIsAny) return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Update", Description = "Yazdığınız Url başka bir oda tarafından kullanılıyor. Lütfen başka bir url yazınız.." } }, 400);
                            getRoom.Slug = urlReplace;
                        }
                    }

                    getRoom.UpdatedAt = DateTime.Now;
                    getRoom.UpdatedById = userId;

                    if ((getRoom.MetaTitle != model.MetaTitle) && model.MetaTitle is not null) getRoom.MetaTitle = model.MetaTitle;
                    if ((getRoom.MetaDescription != model.MetaDescription) && model.MetaDescription is not null) getRoom.MetaDescription = model.MetaDescription;

                    if ((getRoom.Rooms != model.Rooms) && model.Rooms > 0) getRoom.Rooms = model.Rooms;
                    if ((getRoom.Bath != model.Bath) && model.Bath > 0) getRoom.Bath = model.Bath;
                    if ((getRoom.Person != model.Person) && model.Person > 0) getRoom.Person = model.Person;
                    if ((getRoom.OnlineReservation != model.OnlineReservation) && model.OnlineReservation != null) getRoom.OnlineReservation = model.OnlineReservation;

                    if ((getRoom.PriceType != model.PriceType) && model.PriceType > 0) getRoom.PriceType = model.PriceType;
                    if ((getRoom.GeneralStatusType != model.GeneralStatusType) && model.GeneralStatusType > 0) getRoom.GeneralStatusType = model.GeneralStatusType;

                    if ((getRoom.ElectricityMeterNumber != model.ElectricityMeterNumber) && model.ElectricityMeterNumber is not null) getRoom.ElectricityMeterNumber = model.ElectricityMeterNumber;
                    if ((getRoom.InternetMeterNumber != model.InternetMeterNumber) && model.InternetMeterNumber is not null) getRoom.InternetMeterNumber = model.InternetMeterNumber;
                    if ((getRoom.WaterMaterNumber != model.WaterMaterNumber) && model.WaterMaterNumber is not null) getRoom.WaterMaterNumber = model.WaterMaterNumber;
                    if ((getRoom.WifiPassword != model.WifiPassword) && model.WifiPassword is not null) getRoom.WifiPassword = model.WifiPassword;

                    if ((getRoom.Line != model.Line) && model.Line > 0) getRoom.Line = model.Line;

                    context.Rooms.Update(getRoom);
                    await context.SaveChangesAsync();

                    return ResponseDto<BaseResponseDto>.Success(new() { Id = getRoom.Id }, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Update", Description = "Oda Bulunamadı.." } }, 400);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> UpdateDetail(RoomDetailUpdateRequestDto model, Guid userId)
        {
            try
            {
                var getRoomDetail = await context.RoomDetails.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (getRoomDetail != null)
                {
                    getRoomDetail.UpdatedAt = DateTime.Now;
                    getRoomDetail.UpdatedById = userId;

                    if ((getRoomDetail.Name != model.Name) && model.Name is not null) getRoomDetail.Name = model.Name;
                    if ((getRoomDetail.DescriptionShort != model.DescriptionShort) && model.DescriptionShort is not null) getRoomDetail.DescriptionShort = model.DescriptionShort;
                    if ((getRoomDetail.DescriptionLong != model.DescriptionLong) && model.DescriptionLong is not null) getRoomDetail.DescriptionLong = model.DescriptionLong;
                    if ((getRoomDetail.GeneralStatusType != model.GeneralStatusType) && model.GeneralStatusType > 0) getRoomDetail.GeneralStatusType = model.GeneralStatusType;

                    if ((getRoomDetail.FeatureTextWhite != model.FeatureTextWhite) && model.FeatureTextWhite is not null) getRoomDetail.FeatureTextWhite = model.FeatureTextWhite;
                    if ((getRoomDetail.FeatureTextRed != model.FeatureTextRed) && model.FeatureTextRed is not null) getRoomDetail.FeatureTextRed = model.FeatureTextRed;
                    if ((getRoomDetail.FeatureTextBlue != model.FeatureTextBlue) && model.FeatureTextBlue is not null) getRoomDetail.FeatureTextBlue = model.FeatureTextBlue;

                    context.RoomDetails.Update(getRoomDetail);
                    await context.SaveChangesAsync();

                    return ResponseDto<BaseResponseDto>.Success(new() { Id = getRoomDetail.Id }, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Update", Description = "Oda Bulunamadı.." } }, 400);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }
    }
}
