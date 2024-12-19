using LabirentFethiye.Application.Abstracts.ProjectInterfaces;
using LabirentFethiye.Common.Dtos.GlobalDtos.CityDtos.CityResponseDtos;
using LabirentFethiye.Common.Dtos.GlobalDtos.DistrictDtos.DistrictResponseDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.VillaDtos.VillaRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.VillaDtos.VillaResponseDtos;
using LabirentFethiye.Common.Dtos.TownDtos.TownResponseDtos;
using LabirentFethiye.Common.Enums;
using LabirentFethiye.Common.Responses;
using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Contexts;
using LabirentFethiye.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;

namespace LabirentFethiye.Persistence.Concrete.ProjectConcretes
{
    public class VillaService : IVillaService
    {
        private readonly AppDbContext context;

        public VillaService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<ResponseDto<BaseResponseDto>> Create(VillaCreateRequestDto model, Guid userId)
        {
            try
            {
                string urlReplace = GeneralFunctions.UrlReplace(model.Name);
                var urlIsAny = context.Villas.Any(x => x.Slug == urlReplace);
                while (urlIsAny)
                {
                    urlReplace = GeneralFunctions.UrlReplace(urlReplace + "-1");
                    urlIsAny = context.Villas.Any(x => x.Slug == urlReplace);
                }

                Villa villa = new()
                {
                    GeneralStatusType = GeneralStatusType.Active,
                    CreatedAt = DateTime.Now,
                    CreatedById = userId,
                    Room = model.Room,
                    Bath = model.Bath,
                    Person = model.Person,
                    GoogleMap = model.GoogleMap,

                    OnlineReservation = model.OnlineReservation,
                    TownId = model.TownId is not null ? Guid.Parse(model.TownId.ToString()) : Guid.Empty,

                    MetaTitle = model.MetaTitle is not null ? model.MetaTitle : model.Name,
                    MetaDescription = model.MetaDescription is not null ? model.MetaDescription : model.Name,
                    Slug = urlReplace,

                    ElectricityMeterNumber = model.ElectricityMeterNumber,
                    InternetMeterNumber = model.InternetMeterNumber,
                    WaterMaterNumber = model.WaterMaterNumber,
                    WifiPassword = model.WifiPassword,
                    IsRent = model.IsRent,
                    IsSale = model.IsSale,
                    Line = 0,

                    VillaOwnerName = model.VillaOwnerName,
                    VillaOwnerPhone = model.VillaOwnerPhone,
                    VillaNumber = model.VillaNumber,
                    PriceType = model.PriceType,

                    PersonalId = model.PersonalId,

                    VillaDetails = new List<VillaDetail>() {
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

                await context.Villas.AddAsync(villa);
                await context.SaveChangesAsync();

                return ResponseDto<BaseResponseDto>.Success(new() { Id = villa.Id }, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> CreateDetail(VillaDetailCreateRequestDto model, Guid userId)
        {
            try
            {
                VillaDetail villaDetail = new()
                {
                    VillaId = model.VillaId,
                    LanguageCode = model.LanguageCode,
                    Name = model.Name,
                    DescriptionShort = model.DescriptionShort,
                    DescriptionLong = model.DescriptionLong,
                    FeatureTextBlue = model.FeatureTextBlue,
                    FeatureTextRed = model.FeatureTextRed,
                    FeatureTextWhite = model.FeatureTextWhite,
                    CreatedById = userId,
                };

                await context.VillaDetails.AddAsync(villaDetail);
                await context.SaveChangesAsync();

                return ResponseDto<BaseResponseDto>.Success(new() { Id = villaDetail.Id }, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<VillaGetResponseDto>> Get(Guid Id)
        {
            try
            {
                var getVilla = await context.Villas
                    .Include(x => x.VillaDetails)
                    .Include(x => x.Payments)
                    .Include(x => x.VillaCategories).ThenInclude(x => x.Category)
                    .Include(x => x.Town).ThenInclude(x => x.District).ThenInclude(x => x.City)
                    .Include(x => x.Photos.OrderBy(x => x.Line))
                    .Where(x => x.Id == Id)
                    .Select(villa => new VillaGetResponseDto()
                    {
                        Id = villa.Id,
                        Bath = villa.Bath,
                        Room = villa.Room,
                        Person = villa.Person,
                        ElectricityMeterNumber = villa.ElectricityMeterNumber,
                        InternetMeterNumber = villa.InternetMeterNumber,
                        GoogleMap = villa.GoogleMap,
                        GeneralStatusType = villa.GeneralStatusType,
                        IsRent = villa.IsRent,
                        IsSale = villa.IsSale,
                        Line = villa.Line,
                        MetaDescription = villa.MetaDescription,
                        MetaTitle = villa.MetaTitle,
                        OnlineReservation = villa.OnlineReservation,
                        PriceType = villa.PriceType,
                        Slug = villa.Slug,
                        VillaNumber = villa.VillaNumber,
                        VillaOwnerName = villa.VillaOwnerName,
                        VillaOwnerPhone = villa.VillaOwnerPhone,
                        WaterMaterNumber = villa.WaterMaterNumber,
                        WifiPassword = villa.WifiPassword,
                        TownId = villa.TownId,
                        PersonalId = villa.PersonalId,
                        Personal = new VillaGetResponseDtoPersonal()
                        {
                            Id = villa.Personal.Id,
                            Name = villa.Personal.Name,
                            SurName = villa.Personal.SurName,
                            Phone = villa.Personal.PhoneNumber
                        },
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
                        VillaDetails = villa.VillaDetails.Select(villaDetail => new VillaGetResponseDtoVillaDetail()
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
                        }).ToList(),
                        Photos = villa.Photos.Select(photo => new VillaGetResponseDtoPhoto()
                        {
                            Image = photo.Image,
                            ImgAlt = photo.ImgAlt,
                            ImgTitle = photo.ImgTitle,
                            Line = photo.Line,
                            Title = photo.Title,
                            VideoLink = photo.VideoLink
                        }).ToList(),
                        Payments = villa.Payments.Select(payment => new VillaGetResponseDtoPayments()
                        {
                            Amount = payment.Amount,
                            InOrOut = payment.InOrOut
                        }).ToList(),
                        Categories = villa.VillaCategories.Select(category => new VillaGetResponseDtoCategories()
                        {
                            Id = category.CategoryId,
                            CategoryDetails = category.Category.CategoryDetails.Select(categoryDetail => new VillaGetResponseDtoCategoriesDetail()
                            {
                                Name = categoryDetail.Name
                            }).ToList()
                        }).ToList()
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                return ResponseDto<VillaGetResponseDto>.Success(getVilla, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<VillaGetResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<VillaGetAllResponseDto>>> GetAll(VillaGetAllRequestDto model)
        {
            try
            {
                var query = context.Villas
                    .AsQueryable()
                    .Where(x => x.GeneralStatusType == GeneralStatusType.Active);

                if (model.SearchName is not null)
                    query = query.Where(x => x.VillaDetails.Any(x => x.Name.Contains(model.SearchName)));

                if (model.OrderByName is not null)
                    if (model.OrderByName == true) { query = query.OrderBy(x => x.VillaDetails.FirstOrDefault().Name); }
                    else { query = query.OrderByDescending(x => x.VillaDetails.FirstOrDefault().Name); }
                else if (model.OrderByPerson is not null)
                    if (model.OrderByPerson == true) { query = query.OrderBy(x => x.Person); }
                    else { query = query.OrderByDescending(x => x.Person); }
                else if (model.OrderByOnlineReservation is not null)
                    if (model.OrderByOnlineReservation == true) { query = query.OrderBy(x => x.OnlineReservation); }
                    else { query = query.OrderByDescending(x => x.OnlineReservation); }
                else
                    query = query.OrderByDescending(x => x.CreatedAt);

                PageInfo pageInfo = GeneralFunctions.PageInfoHelper(Page: model.Pagination.Page, Size: model.Pagination.Size, TotalCount: await query.CountAsync());

                query = query
                    .Skip(model.Pagination.Page * model.Pagination.Size)
                    .Take(model.Pagination.Size);

                var getAllVilla = await query
                    .Select(villa => new VillaGetAllResponseDto()
                    {
                        Id = villa.Id,
                        VillaNumber = villa.VillaNumber,
                        Bath = villa.Bath,
                        Room = villa.Room,
                        Person = villa.Person,
                        GoogleMap = villa.GoogleMap,
                        GeneralStatusType = villa.GeneralStatusType,
                        OnlineReservation = villa.OnlineReservation,
                        CreatedAt = villa.CreatedAt,
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
                        VillaDetails = villa.VillaDetails.Select(villaDetail => new VillaGetResponseDtoVillaDetail()
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
                    .AsNoTracking()
                    .ToListAsync();



                return ResponseDto<ICollection<VillaGetAllResponseDto>>.Success(getAllVilla, 200, pageInfo);



                //var getAllVilla = context.Villas
                //    .AsQueryable()
                //    .Where(x => x.GeneralStatusType == GeneralStatusType.Active)
                //   .Include(x => x.VillaDetails)
                //   .Select(villa => new VillaGetAllResponseDto()
                //   {
                //       Id = villa.Id,
                //       Bath = villa.Bath,
                //       Room = villa.Room,
                //       Person = villa.Person,
                //       GoogleMap = villa.GoogleMap,
                //       GeneralStatusType = villa.GeneralStatusType,
                //       OnlineReservation = villa.OnlineReservation,
                //       TownId = villa.TownId,
                //       Town = new TownGetResponseDto()
                //       {
                //           Id = villa.Town.Id,
                //           Name = villa.Town.Name,
                //           DistrictId = villa.Town.DistrictId,
                //           District = new DistrictGetResponseDto()
                //           {
                //               Name = villa.Town.District.Name,
                //               CityId = villa.Town.District.City.Id,
                //               City = new CityGetResponseDto()
                //               {
                //                   Name = villa.Town.District.City.Name,
                //                   CityNumber = villa.Town.District.City.CityNumber,
                //               }
                //           }
                //       },
                //       VillaDetails = villa.VillaDetails.Select(villaDetail => new VillaGetResponseDtoVillaDetail()
                //       {
                //           Id = villaDetail.Id,
                //           Name = villaDetail.Name,
                //           DescriptionLong = villaDetail.DescriptionLong,
                //           DescriptionShort = villaDetail.DescriptionShort,
                //           FeatureTextBlue = villaDetail.FeatureTextBlue,
                //           FeatureTextRed = villaDetail.FeatureTextRed,
                //           FeatureTextWhite = villaDetail.FeatureTextWhite,
                //           GeneralStatusType = villaDetail.GeneralStatusType,
                //           LanguageCode = villaDetail.LanguageCode
                //       }).ToList()
                //   });


                //if (model.SearchName is not null)
                //    getAllVilla = getAllVilla.Where(x => x.VillaDetails.Any(x => x.Name.Contains(model.SearchName)));

                //if (model.OrderByName is not null)
                //    if (model.OrderByName == true) { getAllVilla = getAllVilla.OrderBy(x => x.VillaDetails.FirstOrDefault().Name); }
                //    else { getAllVilla = getAllVilla.OrderByDescending(x => x.VillaDetails.FirstOrDefault().Name); }
                //else if (model.OrderByPerson is not null)
                //    if (model.OrderByPerson == true) { getAllVilla = getAllVilla.OrderBy(x => x.Person); }
                //    else { getAllVilla = getAllVilla.OrderByDescending(x => x.Person); }
                //else if (model.OrderByOnlineReservation is not null)
                //    if (model.OrderByOnlineReservation == true) { getAllVilla = getAllVilla.OrderBy(x => x.OnlineReservation); }
                //    else { getAllVilla = getAllVilla.OrderByDescending(x => x.OnlineReservation); }
                //else
                //    getAllVilla = getAllVilla.OrderByDescending(x => x.CreatedAt);

                //int TotalCount = await getAllVilla.CountAsync();

                //getAllVilla = getAllVilla
                //    .Skip(model.Pagination.Page * model.Pagination.Size)
                //   .Take(model.Pagination.Size);

                //PageInfo pageInfo = GeneralFunctions.PageInfoHelper(Page: model.Pagination.Page, Size: model.Pagination.Size, TotalCount: TotalCount);

                //return ResponseDto<ICollection<VillaGetAllResponseDto>>.Success(await getAllVilla.AsNoTracking().ToListAsync(), 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<VillaGetAllResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<VillaGetAllAvailableDateResponseDto>>> GetVillaAvailableDates(Guid VillaId)
        {
            try
            {
                // Todo: Bu servis yapılacak

                List<VillaGetAllAvailableDateResponseDto> responseModel = new();

                var reservations = await context.Reservations
                    .AsNoTracking()
                    .Where(x =>
                        (x.VillaId == VillaId && x.ReservationStatusType != ReservationStatusType.Cancaled) &&
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

                return ResponseDto<ICollection<VillaGetAllAvailableDateResponseDto>>.Success(responseModel, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<VillaGetAllAvailableDateResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> Update(VillaUpdateRequestDto model, Guid userId)
        {
            try
            {
                var getVilla = await context.Villas.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (getVilla != null)
                {
                    if (!String.IsNullOrEmpty(model.Slug))
                    {
                        string urlReplace = GeneralFunctions.UrlReplace(model.Slug);
                        if (getVilla.Slug != urlReplace)
                        {
                            var urlIsAny = context.Villas.Any(x => x.Id != getVilla.Id && x.Slug == urlReplace);
                            if (urlIsAny) return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Update", Description = "Yazdığınız Url başka bir villa tarafından kullanılıyor. Lütfen başka bir url yazınız.." } }, 400);
                            getVilla.Slug = urlReplace;
                        }
                    }

                    getVilla.UpdatedAt = DateTime.Now;
                    getVilla.UpdatedById = userId;

                    if ((getVilla.GoogleMap != model.GoogleMap) && model.GoogleMap is not null) getVilla.GoogleMap = model.GoogleMap;
                    if ((getVilla.MetaTitle != model.MetaTitle) && model.MetaTitle is not null) getVilla.MetaTitle = model.MetaTitle;
                    if ((getVilla.MetaDescription != model.MetaDescription) && model.MetaDescription is not null) getVilla.MetaDescription = model.MetaDescription;

                    if ((getVilla.Room != model.Room) && model.Room > 0) getVilla.Room = model.Room;
                    if ((getVilla.Bath != model.Bath) && model.Bath > 0) getVilla.Bath = model.Bath;
                    if ((getVilla.Person != model.Person) && model.Person > 0) getVilla.Person = model.Person;

                    if ((getVilla.OnlineReservation != model.OnlineReservation) && model.OnlineReservation != null) getVilla.OnlineReservation = model.OnlineReservation;

                    if ((getVilla.GeneralStatusType != model.GeneralStatusType) && model.GeneralStatusType > 0) getVilla.GeneralStatusType = model.GeneralStatusType;

                    if ((getVilla.ElectricityMeterNumber != model.ElectricityMeterNumber) && model.ElectricityMeterNumber is not null) getVilla.ElectricityMeterNumber = model.ElectricityMeterNumber;
                    if ((getVilla.InternetMeterNumber != model.InternetMeterNumber) && model.InternetMeterNumber is not null) getVilla.InternetMeterNumber = model.InternetMeterNumber;
                    if ((getVilla.WaterMaterNumber != model.WaterMaterNumber) && model.WaterMaterNumber is not null) getVilla.WaterMaterNumber = model.WaterMaterNumber;
                    if ((getVilla.WifiPassword != model.WifiPassword) && model.WifiPassword is not null) getVilla.WifiPassword = model.WifiPassword;

                    if ((getVilla.VillaOwnerName != model.VillaOwnerName) && model.VillaOwnerName is not null) getVilla.VillaOwnerName = model.VillaOwnerName;
                    if ((getVilla.VillaOwnerPhone != model.VillaOwnerPhone) && model.VillaOwnerPhone is not null) getVilla.VillaOwnerPhone = model.VillaOwnerPhone;
                    if ((getVilla.VillaNumber != model.VillaNumber) && model.VillaNumber is not null) getVilla.VillaNumber = model.VillaNumber;

                    if ((getVilla.PriceType != model.PriceType) && model.PriceType > 0) getVilla.PriceType = model.PriceType;

                    if (getVilla.IsRent != model.IsRent) getVilla.IsRent = model.IsRent;
                    if (getVilla.IsSale != model.IsSale) getVilla.IsSale = model.IsSale;
                    if ((getVilla.Line != model.Line) && model.Line > 0) getVilla.Line = model.Line;

                    if ((getVilla.PersonalId != model.PersonalId) && model.PersonalId is not null && model.PersonalId != Guid.Empty) getVilla.PersonalId = model.PersonalId;


                    if (getVilla.TownId != model.TownId && model.TownId is not null && model.TownId != Guid.Empty) getVilla.TownId = model.TownId is not null ? Guid.Parse(model.TownId.ToString()) : Guid.Empty;

                    context.Villas.Update(getVilla);
                    var result = await context.SaveChangesAsync();

                    return ResponseDto<BaseResponseDto>.Success(new() { Id = getVilla.Id }, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Update", Description = "Villa Bulunamadı.." } }, 400);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> UpdateDetail(VillaDetailUpdateRequestDto model, Guid userId)
        {
            try
            {
                var getVillaDetail = await context.VillaDetails.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (getVillaDetail != null)
                {
                    getVillaDetail.UpdatedAt = DateTime.Now;
                    getVillaDetail.UpdatedById = userId;

                    if ((getVillaDetail.Name != model.Name) && model.Name is not null) getVillaDetail.Name = model.Name;
                    if ((getVillaDetail.DescriptionShort != model.DescriptionShort) && model.DescriptionShort is not null) getVillaDetail.DescriptionShort = model.DescriptionShort;
                    if ((getVillaDetail.DescriptionLong != model.DescriptionLong) && model.DescriptionLong is not null) getVillaDetail.DescriptionLong = model.DescriptionLong;
                    if ((getVillaDetail.GeneralStatusType != model.GeneralStatusType) && model.GeneralStatusType > 0) getVillaDetail.GeneralStatusType = model.GeneralStatusType;

                    if ((getVillaDetail.FeatureTextWhite != model.FeatureTextWhite) && model.FeatureTextWhite is not null) getVillaDetail.FeatureTextWhite = model.FeatureTextWhite;
                    if ((getVillaDetail.FeatureTextRed != model.FeatureTextRed) && model.FeatureTextRed is not null) getVillaDetail.FeatureTextRed = model.FeatureTextRed;
                    if ((getVillaDetail.FeatureTextBlue != model.FeatureTextBlue) && model.FeatureTextBlue is not null) getVillaDetail.FeatureTextBlue = model.FeatureTextBlue;

                    context.VillaDetails.Update(getVillaDetail);
                    var result = await context.SaveChangesAsync();

                    return ResponseDto<BaseResponseDto>.Success(new() { Id = getVillaDetail.Id }, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "UpdateDetail", Description = "Villa Bulunamadı.." } }, 400);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> VillaCategoryAsign(VillaCategoryAsignRequestDto model)
        {
            try
            {

                List<VillaCategory> ListVillaCategories = await context.VillaCategories
                    .AsNoTracking()
                    .Where(x => x.VillaId == model.VillaId)
                    .ToListAsync();

                context.VillaCategories.RemoveRange(ListVillaCategories);
                //-----

                List<VillaCategory> AddVillaCategories = new List<VillaCategory>();
                foreach (var category in model.CategoryIds)
                {
                    AddVillaCategories.Add(new() { VillaId = model.VillaId, CategoryId = category });
                }
                await context.VillaCategories.AddRangeAsync(AddVillaCategories);
                //-----

                await context.SaveChangesAsync();
                //-----

                return ResponseDto<BaseResponseDto>.Success(new() { Id = model.VillaId }, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }
    }
}
