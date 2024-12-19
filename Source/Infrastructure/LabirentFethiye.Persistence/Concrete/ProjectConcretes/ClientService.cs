using LabirentFethiye.Application.Abstracts.ProjectInterfaces;
using LabirentFethiye.Common.Dtos.GlobalDtos.DistrictDtos.DistrictResponseDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientResponseDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.ReservationDtos.ReservationResponseDtos;
using LabirentFethiye.Common.Dtos.TownDtos.TownResponseDtos;
using LabirentFethiye.Common.Enums;
using LabirentFethiye.Common.Responses;
using LabirentFethiye.Domain.Entities.GlobalEntities;
using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Contexts;
using LabirentFethiye.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System.Xml;

namespace LabirentFethiye.Persistence.Concrete.ProjectConcretes
{
    public class ClientService : IClientService
    {
        private readonly AppDbContext context;
        private readonly IReservationService reservationService;
        private readonly IConfiguration configuration;
        public ClientService(AppDbContext context, IConfiguration configuration, IReservationService reservationService)
        {
            this.context = context;
            this.configuration = configuration;
            this.reservationService = reservationService;
        }

        #region Category
        public async Task<ResponseDto<ICollection<ClientCategoryGetAllResponseDto>>> GetAllCategory(ClientCategoryGetAllRequestDto model)
        {
            try
            {
                ICollection<ClientCategoryGetAllResponseDto> getCategories = await context.Categories
                    .Where(x => x.GeneralStatusType == GeneralStatusType.Active)
                    .Select(category => new ClientCategoryGetAllResponseDto()
                    {
                        Icon = category.Icon,
                        Name = category.CategoryDetails.FirstOrDefault(x => x.LanguageCode == model.Language).Name,
                        Slug = category.Slug
                    })
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<ClientCategoryGetAllResponseDto>>.Success(getCategories, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<ClientCategoryGetAllResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }
        #endregion

        #region Villa

        public async Task<ResponseDto<ICollection<ClientVillaSearchGetAllResponseDto>>> GetAllVillaSearch(ClientVillaSearchGetAllRequestDto model)
        {
            try
            {
                var query = context.Villas
                    .AsQueryable()
                    .Where(x => x.GeneralStatusType == GeneralStatusType.Active);
                //------
                if ((model.Person != null && model.Person > 0) && (model.Name != null && !String.IsNullOrEmpty(model.Name)) && (model.CheckIn != null && model.CheckIn >= DateTime.Now.Date) && (model.CheckOut != null && model.CheckOut > DateTime.Now.Date && model.CheckOut > model.CheckIn))
                {
                    // Hepsi => Villa veya apart adı, Giriş Çıkış Tarihleri, Kapasite
                    List<string> notAvailibleVillaIds = await context.Reservations
                       .AsNoTracking()
                       .Where(x =>
                           x.GeneralStatusType == GeneralStatusType.Active
                           && model.CheckIn < x.CheckOut
                           && model.CheckOut > x.CheckIn
                       )
                       .Select(item => item.VillaId.ToString())
                       .ToListAsync();

                    query = query.Where(x => x.Person >= model.Person && x.VillaDetails.Any(vd => vd.Name.Contains(model.Name)) && !notAvailibleVillaIds.Contains(x.Id.ToString()));
                }
                else if ((model.Person != null && model.Person > 0) && (model.CheckIn != null && model.CheckIn >= DateTime.Now.Date) && (model.CheckOut != null && model.CheckOut > DateTime.Now.Date))
                {
                    // Sadece Giriş Çıkış Tarihleri, Kapasite
                    List<string> notAvailibleVillaIds = await context.Reservations
                       .AsNoTracking()
                       .Where(x =>
                           x.GeneralStatusType == GeneralStatusType.Active
                           && model.CheckIn < x.CheckOut
                           && model.CheckOut > x.CheckIn
                       )
                       .Select(item => item.VillaId.ToString())
                       .ToListAsync();

                    query = query.Where(x => x.Person >= model.Person && !notAvailibleVillaIds.Contains(x.Id.ToString()));
                }
                else if ((model.Person != null && model.Person > 0) && (model.Name != null && !String.IsNullOrEmpty(model.Name)))
                {
                    // Sadece villa yada apart adi, Kapasite
                    query = query.Where(x => x.Person >= model.Person && x.VillaDetails.Any(vd => vd.Name.Contains(model.Name)));
                }
                else if ((model.Name != null && !String.IsNullOrEmpty(model.Name)) && (model.CheckIn != null && model.CheckIn > DateTime.Now.Date) && (model.CheckOut != null && model.CheckOut > DateTime.Now.Date) && (model.Person == null || model.Person == 0))
                {
                    // Sadece villa veya apart adı, Giriş Çıkış Tarihleri
                    List<string> notAvailibleVillaIds = await context.Reservations
                       .AsNoTracking()
                       .Where(x =>
                           x.GeneralStatusType == GeneralStatusType.Active
                           && model.CheckIn < x.CheckOut
                           && model.CheckOut > x.CheckIn
                       )
                       .Select(item => item.VillaId.ToString())
                       .ToListAsync();

                    query = query.Where(x => x.VillaDetails.Any(vd => vd.Name.Contains(model.Name)) && !notAvailibleVillaIds.Contains(x.Id.ToString()));
                }
                else if ((model.Person != null && model.Person > 0))
                {
                    // Sadece Kapasite
                    query = query.Where(x => x.Person >= model.Person);
                }
                else if ((model.CheckIn != null && model.CheckIn >= DateTime.Now.Date) && (model.CheckOut != null && model.CheckOut > DateTime.Now.Date))
                {
                    // Sadece Giriş - Çıkış Tarihleri
                    List<string> notAvailibleVillaIds = await context.Reservations
                        .AsNoTracking()
                        .Where(x =>
                            x.GeneralStatusType == GeneralStatusType.Active
                            && model.CheckIn < x.CheckOut
                            && model.CheckOut > x.CheckIn
                        )
                        .Select(item => item.VillaId.ToString())
                        .ToListAsync();

                    query = query.Where(x => !notAvailibleVillaIds.Contains(x.Id.ToString()));
                    //var ress = context.Reservations.Where(x => x.GeneralStatusType == GeneralStatusType.Active && model.CheckIn < x.CheckOut && model.CheckOut > x.CheckIn)
                    //    .Select(item=>item.VillaId)
                    //    .ToList();

                    //query = query.Where(x=>!ress.Contains(x.Id));

                }
                else if (model.Name is not null)
                {
                    // Sadece villa veya apart adı
                    query = query.Where(x => x.VillaDetails.Any(vd => vd.Name.Contains(model.Name)));
                }
                //else
                //    return ResponseDto<ICollection<ClientVillaSearchGetAllResponseDto>>.Fail(new() { new() { Title = "Kayıt Bulunamadı..", Description = "Aradığınız Kriterlere Göre Tesis Bulunamadı.." } }, 400);

                PageInfo pageInfo = GeneralFunctions.PageInfoHelper(Page: model.Pagination.Page, Size: model.Pagination.Size, TotalCount: await query.CountAsync());

                ICollection<ClientVillaSearchGetAllResponseDto> getVillas = await query
                    .Select(villa => new ClientVillaSearchGetAllResponseDto()
                    {
                        Name = villa.VillaDetails.FirstOrDefault(x => x.LanguageCode == model.Language).Name,
                        Bath = villa.Bath,
                        Room = villa.Room,
                        Person = villa.Person,
                        OnlineReservation = villa.OnlineReservation,
                        Slug = villa.Slug,
                        VillaNumber = villa.VillaNumber,
                        PriceType = villa.PriceType,
                        CategoryMetaTitle = villa.VillaCategories.FirstOrDefault().Category.MetaTitle,
                        CategoryMetaDescription = villa.VillaCategories.FirstOrDefault().Category.MetaDescription,
                        FeatureTextWhite = villa.VillaDetails.FirstOrDefault(x => x.LanguageCode == model.Language).FeatureTextWhite,
                        MinPrice = villa.PriceTables.Count > 0 ? villa.PriceTables.Min(x => x.Price) : 0,
                        MaxPrice = villa.PriceTables.Count > 0 ? villa.PriceTables.Max(x => x.Price) : 0,
                        Town = villa.Town.Name,
                        District = villa.Town.District.Name,
                        Photos = villa.Photos.OrderBy(x => x.Line).Take(3).Select(villaPhoto => new ClientVillaSearchGetAllResponseDtoPhotos()
                        {
                            Image = villaPhoto.Image,
                        }).ToList()
                    })
                    .OrderBy(x => x.Name)
                    .Skip(model.Pagination.Page * model.Pagination.Size)
                    .Take(model.Pagination.Size)
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<ClientVillaSearchGetAllResponseDto>>.Success(getVillas, 200, pageInfo);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<ClientVillaSearchGetAllResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<ClientVillaGetAllByCategorySlugResponseDto>>> GetAllVillaByCategorySlug(ClientVillaGetAllByCategorySlugRequestDto model)
        {
            try
            {
                var query = context.Villas
                    .AsQueryable()
                    .Where(x => x.GeneralStatusType == GeneralStatusType.Active && x.VillaCategories.Any(x => x.Category.Slug == model.Slug));

                query = query.OrderByDescending(x => x.Line);

                query = query.Include(x => x.VillaDetails);

                PageInfo pageInfo = GeneralFunctions.PageInfoHelper(Page: model.Pagination.Page, Size: model.Pagination.Size, TotalCount: await query.CountAsync());

                ICollection<ClientVillaGetAllByCategorySlugResponseDto> getVillas = await query
                    .Select(villa => new ClientVillaGetAllByCategorySlugResponseDto()
                    {
                        Name = villa.VillaDetails.FirstOrDefault(x => x.LanguageCode == model.Language).Name,
                        Bath = villa.Bath,
                        Room = villa.Room,
                        Person = villa.Person,
                        OnlineReservation = villa.OnlineReservation,
                        Slug = villa.Slug,
                        VillaNumber = villa.VillaNumber,
                        PriceType = villa.PriceType,
                        CategoryMetaTitle = villa.VillaCategories.FirstOrDefault().Category.MetaTitle,
                        CategoryMetaDescription = villa.VillaCategories.FirstOrDefault().Category.MetaDescription,
                        FeatureTextWhite = villa.VillaDetails.FirstOrDefault(x => x.LanguageCode == model.Language).FeatureTextWhite,
                        MinPrice = villa.PriceTables.Count > 0 ? villa.PriceTables.Min(x => x.Price) : 0,
                        MaxPrice = villa.PriceTables.Count > 0 ? villa.PriceTables.Max(x => x.Price) : 0,
                        Town = villa.Town.Name,
                        District = villa.Town.District.Name,
                        Photos = villa.Photos.OrderBy(x => x.Line).Take(3).Select(villaPhoto => new ClientVillaGetAllByCategorySlugResponseDtoPhotos()
                        {
                            Image = villaPhoto.Image,
                        }).ToList()
                    })
                    .Skip(model.Pagination.Page * model.Pagination.Size)
                    .Take(model.Pagination.Size)
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<ClientVillaGetAllByCategorySlugResponseDto>>.Success(getVillas, 200, pageInfo);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<ClientVillaGetAllByCategorySlugResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ClientVillaGetBySlugResponseDto>> GetVillaBySlug(ClientVillaGetBySlugRequestDto model)
        {
            try
            {
                var villa = await context.Villas
                    .Where(x => x.GeneralStatusType == GeneralStatusType.Active && x.Slug == model.Slug)
                    .Include(x => x.VillaDetails)
                    .Select(villa => new ClientVillaGetBySlugResponseDto()
                    {
                        Name = villa.VillaDetails.FirstOrDefault(x => x.LanguageCode == model.Language).Name,
                        Room = villa.Room,
                        Person = villa.Person,
                        Bath = villa.Bath,
                        VillaNumber = villa.VillaNumber,
                        PriceType = villa.PriceType,
                        MetaTitle = villa.MetaTitle,
                        MetaDescription = villa.MetaDescription,
                        DescriptionLong = villa.VillaDetails.FirstOrDefault(x => x.LanguageCode == model.Language).DescriptionLong,
                        MinPrice = villa.PriceTables.Count > 0 ? villa.PriceTables.Min(x => x.Price) : 0,
                        MaxPrice = villa.PriceTables.Count > 0 ? villa.PriceTables.Max(x => x.Price) : 0,
                        Town = villa.Town.Name,
                        District = villa.Town.District.Name,
                        Photos = villa.Photos.OrderBy(x => x.Line).Select(villaPhoto => new ClientVillaGetBySlugResponseDtoPhotos()
                        {
                            Image = villaPhoto.Image
                        }).ToList(),
                        Personal = new ClientVillaGetBySlugResponseDtoPersonal()
                        {
                            Name = villa.Personal.Name,
                            SurName = villa.Personal.SurName,
                            Phone = villa.Personal.PhoneNumber,
                        }
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                return ResponseDto<ClientVillaGetBySlugResponseDto>.Success(villa, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ClientVillaGetBySlugResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<ClientDistanceRulerByVillaSlugResponseDto>>> GetAllDistanceRulerByVillaSlug(ClientDistanceRulerByVillaSlugRequestDto model)
        {
            try
            {
                var query = context.DistanceRulers
                    .Where(x => x.GeneralStatusType == GeneralStatusType.Active && x.Villa.Slug == model.Slug)
                    .Include(x => x.DistanceRulerDetails)
                    .OrderByDescending(x => x.Line);


                ICollection<ClientDistanceRulerByVillaSlugResponseDto> getDistanceRulers = await query
                    .Select(distanceRuler => new ClientDistanceRulerByVillaSlugResponseDto()
                    {
                        Icon = distanceRuler.Icon,
                        Value = distanceRuler.Value,
                        Name = distanceRuler.DistanceRulerDetails.FirstOrDefault(x => x.LanguageCode == model.Language).Name
                    })
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<ClientDistanceRulerByVillaSlugResponseDto>>.Success(getDistanceRulers, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<ClientDistanceRulerByVillaSlugResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<ClientVillaGetAllResponseDto>>> GetAllVilla(ClientVillaGetAllRequestDto model)
        {
            try
            {
                var query = context.Villas
                    .AsQueryable()
                    .Where(x => x.GeneralStatusType == GeneralStatusType.Active);

                query = query.OrderByDescending(x => x.Line);

                PageInfo pageInfo = GeneralFunctions.PageInfoHelper(Page: model.Pagination.Page, Size: model.Pagination.Size, TotalCount: await query.CountAsync());

                query = query
                    .Include(x => x.VillaDetails)
                    .Include(x => x.PriceTables);

                ICollection<ClientVillaGetAllResponseDto> getVillas = await query
                    .Select(villa => new ClientVillaGetAllResponseDto()
                    {
                        Name = villa.VillaDetails.FirstOrDefault(x => x.LanguageCode == model.Language).Name,
                        Bath = villa.Bath,
                        Room = villa.Room,
                        Person = villa.Person,
                        OnlineReservation = villa.OnlineReservation,
                        Slug = villa.Slug,
                        VillaNumber = villa.VillaNumber,
                        PriceType = villa.PriceType,
                        CategoryMetaTitle = "Fethiye Kiralık Villalar | Lüks ve Konforlu Tatil Seçenekleri",
                        CategoryMetaDescription = "Fethiye'de lüks ve konforlu haftalık kiralık villalar! Eşsiz doğa manzaraları, özel havuzlar ve modern olanaklarla dolu villalarımızda unutulmaz bir tatil deneyimi yaşayın. Hemen rezervasyon yapın ve Fethiye'nin güzelliklerini keşfedin!",
                        FeatureTextWhite = villa.VillaDetails.FirstOrDefault().FeatureTextWhite,
                        MinPrice = villa.PriceTables.Count > 0 ? villa.PriceTables.Min(x => x.Price) : 0,
                        MaxPrice = villa.PriceTables.Count > 0 ? villa.PriceTables.Max(x => x.Price) : 0,
                        Town = villa.Town.Name,
                        District = villa.Town.District.Name,
                        Photos = villa.Photos.OrderBy(x => x.Line).Take(3).Select(villaPhoto => new ClientVillaGetAllResponseDtoPhotos()
                        {
                            Image = villaPhoto.Image,
                        }).ToList()
                    })
                    .Skip(model.Pagination.Page * model.Pagination.Size)
                    .Take(model.Pagination.Size)
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<ClientVillaGetAllResponseDto>>.Success(getVillas, 200, pageInfo);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<ClientVillaGetAllResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ClientVillaGetResponseDto>> GetVilla(ClientVillaGetRequestDto model)
        {
            try
            {
                var getVilla = await context.Villas
                    .Where(x => x.GeneralStatusType == GeneralStatusType.Active)
                    .OrderByDescending(x => x.Line)
                    .Include(x => x.VillaDetails)
                    .Select(villa => new ClientVillaGetResponseDto()
                    {
                        Name = villa.VillaDetails.FirstOrDefault(x => x.LanguageCode == model.Language).Name,
                        Bath = villa.Bath,
                        Room = villa.Room,
                        Person = villa.Person,
                        OnlineReservation = villa.OnlineReservation,
                        Slug = villa.Slug,
                        DescriptionLong = villa.VillaDetails.FirstOrDefault(x => x.LanguageCode == model.Language).DescriptionLong,
                        VillaNumber = villa.VillaNumber,
                        PriceType = villa.PriceType,
                        CategoryMetaTitle = "Fethiye Kiralık Villalar | Lüks ve Konforlu Tatil Seçenekleri",
                        CategoryMetaDescription = "Fethiye'de lüks ve konforlu haftalık kiralık villalar! Eşsiz doğa manzaraları, özel havuzlar ve modern olanaklarla dolu villalarımızda unutulmaz bir tatil deneyimi yaşayın. Hemen rezervasyon yapın ve Fethiye'nin güzelliklerini keşfedin!",
                        FeatureTextWhite = villa.VillaDetails.FirstOrDefault(x => x.LanguageCode == model.Language).FeatureTextWhite,
                        MinPrice = villa.PriceTables.Count > 0 ? villa.PriceTables.Min(x => x.Price) : 0,
                        MaxPrice = villa.PriceTables.Count > 0 ? villa.PriceTables.Max(x => x.Price) : 0,
                        Town = villa.Town.Name,
                        District = villa.Town.District.Name,
                        Photos = villa.Photos.OrderBy(x => x.Line).Select(villaPhoto => new ClientVillaGetResponseDtoPhotos()
                        {
                            Image = villaPhoto.Image,
                        }).ToList()
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                return ResponseDto<ClientVillaGetResponseDto>.Success(getVilla, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ClientVillaGetResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<ClientPriceTableGetAllByVillaSlugResponseDto>>> GetAllPriceTableByVillaSlug(ClientPriceTableGetAllByVillaSlugRequestDto model)
        {
            try
            {
                var query = context.PriceTables
                    .Where(x => x.GeneralStatusType == GeneralStatusType.Active && x.Villa.Slug == model.Slug)
                    .Include(x => x.PriceTableDetails)
                    .OrderByDescending(x => x.Line);


                ICollection<ClientPriceTableGetAllByVillaSlugResponseDto> getPriceTables = await query
                    .Select(priceTable => new ClientPriceTableGetAllByVillaSlugResponseDto()
                    {
                        Icon = priceTable.Icon,
                        Title = priceTable.PriceTableDetails.FirstOrDefault(x => x.LanguageCode == model.Language).Title,
                        Description = priceTable.PriceTableDetails.FirstOrDefault(x => x.LanguageCode == model.Language).Description,
                        Price = priceTable.Price
                    })
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<ClientPriceTableGetAllByVillaSlugResponseDto>>.Success(getPriceTables, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<ClientPriceTableGetAllByVillaSlugResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<ClientReservationCalendarGetByVillaSlugResponseDto>>> GetReservationCalendarByVillaSlug(ClientReservationCalendarGetByVillaSlugRequestDto model)
        {
            try
            {
                var query = context.Reservations
                    .Where(x => x.GeneralStatusType == GeneralStatusType.Active && x.Villa.Slug == model.Slug && x.CheckIn >= DateTime.Now.Date && x.ReservationStatusType != ReservationStatusType.Cancaled)
                    .OrderBy(x => x.CheckIn);

                ICollection<ClientReservationCalendarGetByVillaSlugResponseDto> getReservations = await query
                    .Select(reservation => new ClientReservationCalendarGetByVillaSlugResponseDto()
                    {
                        CheckIn = reservation.CheckIn,
                        CheckOut = reservation.CheckOut,
                        ReservationStatusType = reservation.ReservationStatusType
                    })
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<ClientReservationCalendarGetByVillaSlugResponseDto>>.Success(getReservations, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<ClientReservationCalendarGetByVillaSlugResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<ClientPriceDateGetAllByVillaSlugResponseDto>>> GetAllPriceDateByVillaSlug(ClientPriceDateGetAllByVillaSlugRequestDto model)
        {
            try
            {
                ICollection<ClientPriceDateGetAllByVillaSlugResponseDto> getPriceDates = await context.PriceDates
                    .Where(x => x.Villa.Slug == model.Slug && x.EndDate.Date >= DateTime.Now.Date)
                    .Select(price => new ClientPriceDateGetAllByVillaSlugResponseDto()
                    {
                        StartDate = price.StartDate,
                        EndDate = price.EndDate,
                        Price = price.Price
                    })
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<ClientPriceDateGetAllByVillaSlugResponseDto>>.Success(getPriceDates, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<ClientPriceDateGetAllByVillaSlugResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<ClientCommentGetAllByVillaSlugResponseDto>>> GetAllCommentByVillaSlug(ClientCommentGetAllByVillaSlugRequestDto model)
        {
            try
            {
                var query = context.Comments
                    .Where(x => x.GeneralStatusType == GeneralStatusType.Active && x.Villa.Slug == model.Slug)
                    .OrderByDescending(x => x.CreatedAt);

                ICollection<ClientCommentGetAllByVillaSlugResponseDto> getComments = await query
                    .Select(comment => new ClientCommentGetAllByVillaSlugResponseDto()
                    {
                        Title = comment.Title,
                        CommentText = comment.CommentText,
                        Rating = comment.Rating,
                        CreatedAt = comment.CreatedAt,
                        Video = comment.Video,
                        Name = comment.Name,
                        SurName = comment.SurName
                    })
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<ClientCommentGetAllByVillaSlugResponseDto>>.Success(getComments, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<ClientCommentGetAllByVillaSlugResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<ClientRecommendedVillaGetAllByVillaSlugResponseDto>>> GetAllRecommendedVilla(ClientRecommendedVillaGetAllByVillaSlugRequestDto model)
        {
            try
            {
                ICollection<ClientRecommendedVillaGetAllByVillaSlugResponseDto> getVillas = await context.Villas
                    .Where(x => x.GeneralStatusType == GeneralStatusType.Active)
                    .OrderByDescending(x => x.Line)
                    .Select(villa => new ClientRecommendedVillaGetAllByVillaSlugResponseDto()
                    {
                        Name = villa.VillaDetails.FirstOrDefault(x => x.LanguageCode == model.Language).Name,
                        Bath = villa.Bath,
                        Room = villa.Room,
                        Person = villa.Person,
                        OnlineReservation = villa.OnlineReservation,
                        Slug = villa.Slug,
                        VillaNumber = villa.VillaNumber,
                        PriceType = villa.PriceType,
                        FeatureTextWhite = villa.VillaDetails.FirstOrDefault(x => x.LanguageCode == model.Language).FeatureTextWhite,
                        MinPrice = villa.PriceTables.Count > 0 ? villa.PriceTables.Min(x => x.Price) : 0,
                        MaxPrice = villa.PriceTables.Count > 0 ? villa.PriceTables.Max(x => x.Price) : 0,
                        Town = villa.Town.Name,
                        District = villa.Town.District.Name,
                        Photos = villa.Photos.Take(3).Select(villaPhoto => new ClientRecommendedVillaGetAllByVillaSlugResponseDtoPhotos()
                        {
                            Image = villaPhoto.Image,
                        }).ToList()
                    })
                    .Skip(0)
                    .Take(5)
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<ClientRecommendedVillaGetAllByVillaSlugResponseDto>>.Success(getVillas, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<ClientRecommendedVillaGetAllByVillaSlugResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> CreateComment(ClientCommentCreateRequestDto model)
        {
            try
            {
                Comment comment = new()
                {
                    GeneralStatusType = GeneralStatusType.Passive,
                    CreatedAt = DateTime.Now,
                    VillaId = model.VillaSlug is not null ? context.Villas.SingleOrDefaultAsync(x => x.Slug == model.VillaSlug).Result.Id : null,
                    HotelId = model.HotelSlug is not null ? context.Hotels.SingleOrDefaultAsync(x => x.Slug == model.HotelSlug).Result.Id : null,
                    Title = model.Title,
                    CommentText = model.CommentText,
                    Rating = model.Rating,
                    Name = model.Name,
                    SurName = model.SurName,
                    Email = model.Email,
                    Phone = model.Phone
                };

                await context.Comments.AddAsync(comment);
                var result = await context.SaveChangesAsync();
                //------

                return ResponseDto<BaseResponseDto>.Success(new() { Id = comment.Id }, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<ClientVillaSaleGetAllResponseDto>>> GetAllVillaSale(ClientVillaSaleGetAllRequestDto model)
        {
            try
            {
                ICollection<ClientVillaSaleGetAllResponseDto> getVillas = await context.Villas
                    .Where(x => x.GeneralStatusType == GeneralStatusType.Active && x.IsSale == true)
                    .OrderByDescending(x => x.Line)
                    .Select(villa => new ClientVillaSaleGetAllResponseDto()
                    {
                        Name = villa.VillaDetails.FirstOrDefault(x => x.LanguageCode == model.Language).Name,
                        Bath = villa.Bath,
                        Room = villa.Room,
                        Person = villa.Person,
                        Slug = villa.Slug,
                        VillaNumber = villa.VillaNumber,
                        FeatureTextWhite = villa.VillaDetails.FirstOrDefault(x => x.LanguageCode == model.Language).FeatureTextWhite,
                        Town = villa.Town.Name,
                        District = villa.Town.District.Name,
                        Photos = villa.Photos.Take(3).Select(villaPhoto => new ClientVillaSaleGetAllResponseDtoPhotos()
                        {
                            Image = villaPhoto.Image,
                        }).ToList()
                    })
                    .Skip(0)
                    .Take(5)
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<ClientVillaSaleGetAllResponseDto>>.Success(getVillas, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<ClientVillaSaleGetAllResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        #endregion

        #region Hotel Room

        public async Task<ResponseDto<ClientHotelGetResponseDto>> GetHotel(ClientHotelGetRequestDto model)
        {
            try
            {
                var getHotel = await context.Hotels
                    .Where(x => x.Slug == model.Slug && x.GeneralStatusType == GeneralStatusType.Active)
                    .OrderByDescending(x => x.Line)
                    .Include(x => x.HotelDetails)
                    .Select(hotel => new ClientHotelGetResponseDto()
                    {
                        Name = hotel.HotelDetails.FirstOrDefault(x => x.LanguageCode == model.Language).Name,
                        Bath = hotel.Bath,
                        Room = hotel.Room,
                        Person = hotel.Person,
                        Slug = hotel.Slug,
                        PriceType = hotel.PriceType,
                        DescriptionLong = hotel.HotelDetails.FirstOrDefault(x => x.LanguageCode == model.Language).DescriptionLong,
                        MetaTitle = hotel.MetaTitle,
                        MetaDescription = hotel.MetaDescription,
                        MinPrice = hotel.Rooms.SelectMany(room => room.PriceTables).Min(x => x.Price),
                        MaxPrice = hotel.Rooms.SelectMany(room => room.PriceTables).Max(x => x.Price),
                        Town = hotel.Town.Name,
                        District = hotel.Town.District.Name,
                        Photos = hotel.Photos.OrderBy(x => x.Line).Select(hotelPhoto => new ClientHotelGetResponseDtoPhotos()
                        {
                            Image = hotelPhoto.Image,
                        }).ToList(),
                        Rooms = hotel.Rooms.Select(room => new ClientHotelGetResponseDtoRoom()
                        {
                            Name = room.RoomDetails.FirstOrDefault(x => x.LanguageCode == model.Language).Name,
                            Bath = room.Bath,
                            Rooms = room.Rooms,
                            Person = room.Person,
                            FeatureTextWhite = room.RoomDetails.FirstOrDefault(x => x.LanguageCode == model.Language).FeatureTextWhite,
                            OnlineReservation = room.OnlineReservation,
                            //PriceType=room.PriceType,
                            PriceType = hotel.PriceType,
                            Slug = room.Slug,
                            Photos = room.Photos.OrderBy(x => x.Line).Take(3).Select(roomPhoto => new ClientHotelGetResponseDtoRoomPhotos()
                            {
                                Image = roomPhoto.Image
                            }).ToList()
                        }).ToList()
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                return ResponseDto<ClientHotelGetResponseDto>.Success(getHotel, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ClientHotelGetResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ClientRoomGetResponseDto>> GetRoom(ClientRoomGetRequestDto model)
        {
            try
            {
                var getRoom = await context.Rooms
                    .Where(x => x.Slug == model.Slug && x.Hotel.GeneralStatusType == GeneralStatusType.Active && x.GeneralStatusType == GeneralStatusType.Active)
                    .OrderByDescending(x => x.Line)
                    .Include(x => x.RoomDetails)
                    .Select(room => new ClientRoomGetResponseDto()
                    {
                        Name = room.RoomDetails.FirstOrDefault(x => x.LanguageCode == model.Language).Name,
                        Bath = room.Bath,
                        Rooms = room.Rooms,
                        Person = room.Person,
                        Slug = room.Slug,
                        PriceType = room.PriceType,
                        MinPrice = room.PriceTables.Min(x => x.Price),
                        MaxPrice = room.PriceTables.Max(x => x.Price),
                        Town = room.Hotel.Town.Name,
                        District = room.Hotel.Town.District.Name,
                        Photos = room.Photos.OrderBy(x => x.Line).Take(3).Select(roomPhoto => new ClientRoomGetResponseDtoPhotos()
                        {
                            Image = roomPhoto.Image,
                        }).ToList()
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                return ResponseDto<ClientRoomGetResponseDto>.Success(getRoom, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ClientRoomGetResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<ClientHotelGetAllResponseDto>>> GetAllHotel(ClientHotelGetAllRequestDto model)
        {
            try
            {
                ICollection<ClientHotelGetAllResponseDto> getHotels = await context.Hotels
                    .Where(x => x.GeneralStatusType == GeneralStatusType.Active)
                    .OrderByDescending(x => x.Line)
                    .Include(x => x.HotelDetails)
                    .Select(hotel => new ClientHotelGetAllResponseDto()
                    {
                        Name = hotel.HotelDetails.FirstOrDefault(x => x.LanguageCode == model.Language).Name,
                        Bath = hotel.Bath,
                        Room = hotel.Room,
                        Person = hotel.Person,
                        Slug = hotel.Slug,
                        PriceType = hotel.PriceType,
                        FeatureTextWhite = hotel.HotelDetails.FirstOrDefault(x => x.LanguageCode == model.Language).FeatureTextWhite,
                        MinPrice = hotel.Rooms.SelectMany(room => room.PriceTables).Min(x => x.Price),
                        MaxPrice = hotel.Rooms.SelectMany(room => room.PriceTables).Max(x => x.Price),
                        Town = hotel.Town.Name,
                        District = hotel.Town.District.Name,
                        Photos = hotel.Photos.OrderBy(x => x.Line).Take(3).Select(hotelPhoto => new ClientHotelGetAllResponseDtoPhotos()
                        {
                            Image = hotelPhoto.Image,
                        }).ToList()
                    })
                    .Skip(model.Pagination.Page * model.Pagination.Size)
                    .Take(model.Pagination.Size)
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<ClientHotelGetAllResponseDto>>.Success(getHotels, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<ClientHotelGetAllResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<ClientDistanceRulerByHotelSlugResponseDto>>> GetAllDistanceRulerByHotelSlug(ClientDistanceRulerByHotelSlugRequestDto model)
        {
            try
            {
                var query = context.DistanceRulers
                    .Where(x => x.GeneralStatusType == GeneralStatusType.Active && x.Hotel.Slug == model.Slug)
                    .Include(x => x.DistanceRulerDetails)
                    .OrderByDescending(x => x.Line);


                ICollection<ClientDistanceRulerByHotelSlugResponseDto> getDistanceRulers = await query
                    .Select(distanceRuler => new ClientDistanceRulerByHotelSlugResponseDto()
                    {
                        Icon = distanceRuler.Icon,
                        Value = distanceRuler.Value,
                        Name = distanceRuler.DistanceRulerDetails.FirstOrDefault(x => x.LanguageCode == model.Language).Name
                    })
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<ClientDistanceRulerByHotelSlugResponseDto>>.Success(getDistanceRulers, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<ClientDistanceRulerByHotelSlugResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<ClientPriceTableGetAllByRoomSlugResponseDto>>> GetAllPriceTableByRoomSlug(ClientPriceTableGetAllByRoomSlugRequestDto model)
        {
            try
            {
                var query = context.PriceTables
                    .Where(x => x.GeneralStatusType == GeneralStatusType.Active && x.Room.Slug == model.Slug)
                    .Include(x => x.PriceTableDetails)
                    .OrderByDescending(x => x.Line);

                ICollection<ClientPriceTableGetAllByRoomSlugResponseDto> getPriceTables = await query
                    .Select(priceTable => new ClientPriceTableGetAllByRoomSlugResponseDto()
                    {
                        Icon = priceTable.Icon,
                        Title = priceTable.PriceTableDetails.FirstOrDefault(x => x.LanguageCode == model.Language).Title,
                        Description = priceTable.PriceTableDetails.FirstOrDefault(x => x.LanguageCode == model.Language).Description,
                        Price = priceTable.Price
                    })
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<ClientPriceTableGetAllByRoomSlugResponseDto>>.Success(getPriceTables, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<ClientPriceTableGetAllByRoomSlugResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<ClientReservationCalendarGetByRoomSlugResponseDto>>> GetReservationCalendarByRoomSlug(ClientReservationCalendarGetByRoomSlugRequestDto model)
        {
            try
            {
                var query = context.Reservations
                    .Where(x => x.GeneralStatusType == GeneralStatusType.Active && x.Room.Slug == model.Slug && x.CheckIn >= DateTime.Now.Date && x.ReservationStatusType != ReservationStatusType.Cancaled)
                    .OrderBy(x => x.CheckIn);

                ICollection<ClientReservationCalendarGetByRoomSlugResponseDto> getReservations = await query
                    .Select(reservation => new ClientReservationCalendarGetByRoomSlugResponseDto()
                    {
                        CheckIn = reservation.CheckIn,
                        CheckOut = reservation.CheckOut,
                        ReservationStatusType = reservation.ReservationStatusType
                    })
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<ClientReservationCalendarGetByRoomSlugResponseDto>>.Success(getReservations, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<ClientReservationCalendarGetByRoomSlugResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<ClientCommentGetAllByHotelSlugResponseDto>>> GetAllCommentByHotelSlug(ClientCommentGetAllByHotelSlugRequestDto model)
        {
            try
            {
                var query = context.Comments
                    .Where(x => x.GeneralStatusType == GeneralStatusType.Active && x.Hotel.Slug == model.Slug)
                    .OrderByDescending(x => x.CreatedAt);

                ICollection<ClientCommentGetAllByHotelSlugResponseDto> getComments = await query
                    .Select(comment => new ClientCommentGetAllByHotelSlugResponseDto()
                    {
                        Title = comment.Title,
                        CommentText = comment.CommentText,
                        Rating = comment.Rating,
                        CreatedAt = comment.CreatedAt,
                        Video = comment.Video,
                        Name = comment.Name,
                        SurName = comment.SurName
                    })
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<ClientCommentGetAllByHotelSlugResponseDto>>.Success(getComments, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<ClientCommentGetAllByHotelSlugResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        #endregion

        #region Reservation
        public async Task<ResponseDto<ClientReservationGetResponseDto>> GetReservation(ClientReservationGetRequestDto model)
        {
            try
            {
                var getReservaation = await context.Reservations
                    .Where(x => x.ReservationNumber == model.ReservationNumber)
                    .Include(x => x.Villa).ThenInclude(x => x.VillaDetails.Where(x => x.LanguageCode == model.Language))
                    .Include(x => x.Room).ThenInclude(x => x.RoomDetails.Where(x => x.LanguageCode == model.Language))
                    .Select(reservation => new ClientReservationGetResponseDto()
                    {
                        ReservationStatusType = reservation.ReservationStatusType,
                        CheckIn = reservation.CheckIn,
                        CheckOut = reservation.CheckOut,
                        Amount = reservation.Amount,
                        ExtraPrice = reservation.ExtraPrice,
                        Discount = reservation.Discount,
                        Total = reservation.Total,
                        ReservationNumber = reservation.ReservationNumber,
                        ReservationInfos = reservation.ReservationInfos.Select(reservationInfo => new ClientReservationGetResponseDtoReservationInfo()
                        {
                            Name = reservationInfo.Name,
                            Surname = reservationInfo.Surname,
                            Phone = reservationInfo.Phone,
                            Email = reservationInfo.Email,
                            Owner = reservationInfo.Owner
                        }).ToList(),
                        ReservationItems = reservation.ReservationItems.Select(reservationItem => new ClientReservationGetResponseDtoReservationItem()
                        {
                            Day = reservationItem.Day,
                            Price = reservationItem.Price
                        }).ToList(),
                        Villa = new ClientReservationGetResponseDtoVilla()
                        {
                            Name = reservation.Villa.VillaDetails.FirstOrDefault().Name,
                            Slug = reservation.Villa.Slug,
                            Photo = reservation.Villa.Photos.FirstOrDefault().Image
                        },
                        Room = new ClientReservationGetResponseDtoRoom()
                        {
                            Name = reservation.Room.RoomDetails.FirstOrDefault().Name,
                            Slug = reservation.Room.Slug,
                            Photo = reservation.Room.Photos.FirstOrDefault().Image
                        }
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                return ResponseDto<ClientReservationGetResponseDto>.Success(getReservaation, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ClientReservationGetResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ClientReservationCreateResponseDto>> ReservationCreate(ClientReservationCreateRequestDto model)
        {
            using (IDbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (model.CheckIn.Date < DateTime.Now.Date)
                        return ResponseDto<ClientReservationCreateResponseDto>.Fail(new() { new() { Title = "CheckIn Tarihi", Description = "CheckIn Tarihi geçmiş gün olamaz.." } }, 400);
                    if (model.CheckIn.Date > model.CheckOut.Date)
                        return ResponseDto<ClientReservationCreateResponseDto>.Fail(new() { new() { Title = "CheckIn-CheckOut Tarihi", Description = "CheckIn Tarihi CheckOut tarihinden büyük olamaz.." } }, 400);
                    if (model.CheckIn.Date == model.CheckOut.Date)
                        return ResponseDto<ClientReservationCreateResponseDto>.Fail(new() { new() { Title = "CheckIn-CheckOut Tarihi", Description = "CheckIn Tarihi CheckOut tarihine eşit olamaz.." } }, 400);
                    if (model.VillaId == Guid.Empty || model.RoomId == Guid.Empty)
                        return ResponseDto<ClientReservationCreateResponseDto>.Fail(new() { new() { Title = "Tesis Id", Description = "Tesis Id boş olamaz" } }, 400);
                    if ((model.CheckOut.Date - model.CheckIn.Date).Days < 5)
                        return ResponseDto<ClientReservationCreateResponseDto>.Fail(new() { new() { Title = "Night Limit", Description = "En Az 5 Gece Rezervasyon Yapabilirsiniz." } }, 400);
                    //------

                    //if (await reservationService.IsAvailible(new() { CheckIn = model.CheckIn, CheckOut = model.CheckOut, VillaId = model.VillaId, RoomId = model.RoomId }))
                    //    return ResponseDto<ClientReservationCreateResponseDto>.Fail(new() { new() { Title = "Tesis Müsait Değil", Description = "Tesis Belirtilen Tarihler İçin Müsait Değil.." } }, 400);
                    var reservationIsAvailible = await ReservationIsAvailible(new() { CheckIn = model.CheckIn, CheckOut = model.CheckOut, VillaId = model.VillaId, RoomId = model.RoomId });

                    if ((await ReservationIsAvailible(new() { CheckIn = model.CheckIn, CheckOut = model.CheckOut, VillaId = model.VillaId, RoomId = model.RoomId })).Errors?.Count > 0)
                        return ResponseDto<ClientReservationCreateResponseDto>.Fail(new() { new() { Title = "Tesis Müsait Değil", Description = "Tesis Belirtilen Tarihler İçin Müsait Değil.." } }, 400);
                    //-----

                    var prices = await reservationService.GetReservationPrice(new() { VillaId = model.VillaId, RoomId = model.RoomId, CheckIn = model.CheckIn, CheckOut = model.CheckOut });
                    if (prices.StatusCode != 200) return ResponseDto<ClientReservationCreateResponseDto>.Fail(new() { new() { Title = "Tesis Müsait Değil", Description = "Tesise Ait Fiyat Bulunamadı.." } }, 400);
                    //-----

                    decimal total = prices.Data.Total;
                    Reservation reservation = new()
                    {
                        ReservationNumber = GeneralFunctions.UniqueNumber(),

                        CheckIn = model.CheckIn,
                        CheckOut = model.CheckOut,
                        ReservationChannalType = ReservationChannalType.WebSite,
                        ReservationStatusType = ReservationStatusType.Option,
                        Note = model.Note,
                        HomeOwner = false,

                        Amount = prices.Data.Amount,
                        PriceType = prices.Data.PriceType,
                        ExtraPrice = prices.Data.ExtraPrice,
                        Discount = 0,
                        Total = total,

                        RoomId = model.RoomId,
                        VillaId = model.VillaId,
                        GeneralStatusType = GeneralStatusType.Active,
                        CreatedAt = DateTime.Now,

                        ReservationInfos = new List<ReservationInfo>()
                            {
                                new()
                                {
                                    IdNo = model.IdNo,
                                    Name = model.Name,
                                    Surname = model.Surname,
                                    Email = model.Email,
                                    Phone = model.Phone,
                                    PeopleType = PeopleType.Adult,
                                    GeneralStatusType = GeneralStatusType.Active,
                                    Owner = true,
                                    CreatedAt = DateTime.Now
                                }
                            }
                    };

                    await context.Reservations.AddAsync(reservation);
                    var result = await context.SaveChangesAsync();
                    //------

                    List<ReservationItem> reservationItems = new();
                    foreach (var day in prices.Data.Days)
                    {
                        reservationItems.Add(new() { Day = Convert.ToDateTime(day.Day), Price = day.Price, GeneralStatusType = GeneralStatusType.Active, ReservationId = reservation.Id, CreatedAt = DateTime.UtcNow });
                    }
                    await context.ReservationItems.AddRangeAsync(reservationItems);
                    await context.SaveChangesAsync();
                    //------

                    await transaction.CommitAsync();
                    //------

                    return ResponseDto<ClientReservationCreateResponseDto>.Success(new() { ReservationNumber = reservation.ReservationNumber }, 200);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return ResponseDto<ClientReservationCreateResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
                }
            }

        }

        public async Task<ResponseDto<ClientReservationIsAvailibleResponseDto>> ReservationIsAvailible(ClientReservationIsAvailibleRequestDto model)
        {
            try
            {
                if (model.CheckIn.Date < DateTime.Now.Date)
                    return ResponseDto<ClientReservationIsAvailibleResponseDto>.Fail(new() { new() { Title = "CheckIn Tarihi", Description = "CheckIn Tarihi geçmiş gün olamaz.." } }, 400);
                if (model.CheckIn.Date > model.CheckOut.Date)
                    return ResponseDto<ClientReservationIsAvailibleResponseDto>.Fail(new() { new() { Title = "CheckIn-CheckOut Tarihi", Description = "CheckIn Tarihi CheckOut tarihinden büyük olamaz.." } }, 400);
                if (model.CheckIn.Date == model.CheckOut.Date)
                    return ResponseDto<ClientReservationIsAvailibleResponseDto>.Fail(new() { new() { Title = "CheckIn-CheckOut Tarihi", Description = "CheckIn Tarihi CheckOut tarihine eşit olamaz.." } }, 400);
                if (model.VillaId == Guid.Empty || model.RoomId == Guid.Empty)
                    return ResponseDto<ClientReservationIsAvailibleResponseDto>.Fail(new() { new() { Title = "Tesis Id", Description = "Tesis Id boş olamaz" } }, 400);
                if ((model.CheckOut.Date - model.CheckIn.Date).Days < 5)
                    return ResponseDto<ClientReservationIsAvailibleResponseDto>.Fail(new() { new() { Title = "Night Limit", Description = "En Az 5 Gece Rezervasyon Yapabilirsiniz." } }, 400);
                //------

                ClientReservationIsAvailibleResponseDto response;
                var resultIsAvailible = await reservationService.IsAvailible(new() { CheckIn = model.CheckIn, CheckOut = model.CheckOut, VillaId = model.VillaId, RoomId = model.RoomId });
                if (resultIsAvailible)
                {
                    var prices = await reservationService.GetReservationPrice(new() { VillaId = model.VillaId, RoomId = model.RoomId, CheckIn = model.CheckIn, CheckOut = model.CheckOut });
                    if (prices.StatusCode != 200) return ResponseDto<ClientReservationIsAvailibleResponseDto>.Fail(new() { new() { Title = "Tesis Müsait Değil", Description = "Tesise Ait Fiyat Bulunamadı.." } }, 400);

                    response = new()
                    {
                        IsAvailible = true,
                        PriceType = prices.Data.PriceType,
                        TotalPrice = prices.Data.Total,
                        PriceDetails = prices.Data.Days.Select(day => new ClientReservationIsAvailibleResponseDtoPriceDetails
                        {
                            Date = Convert.ToDateTime(day.Day),
                            Price = day.Price
                        })
                        .ToList()
                    };
                }
                else return ResponseDto<ClientReservationIsAvailibleResponseDto>.Fail(new() { new() { Title = "Tesis Müsait Değil", Description = "Tesis Belirtilen Tarihlerde Müsait Değil.." } }, 400);


                return ResponseDto<ClientReservationIsAvailibleResponseDto>.Success(response, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ClientReservationIsAvailibleResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        #endregion

        #region Currency
        public async Task<ResponseDto<ClientCurrencyGetResponseDto>> GetCurrency()
        {
            try
            {
                string path = configuration.GetValue<object>("CurrenciesPath").ToString();
                ClientCurrencyGetResponseDto response = new();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path);
                XmlNodeList nodes = xmlDoc.SelectNodes("/Tarih_Date/Currency");
                foreach (XmlNode currencyNode in nodes)
                {
                    if (currencyNode.ChildNodes[0].InnerText == "USD")
                        response.Usd = Convert.ToDecimal(currencyNode.ChildNodes[2].InnerText.Replace(".", ","));
                    else if (currencyNode.ChildNodes[0].InnerText == "EUR")
                        response.Eur = Convert.ToDecimal(currencyNode.ChildNodes[2].InnerText.Replace(".", ","));
                    else if (currencyNode.ChildNodes[0].InnerText == "GBP")
                        response.Gbp = Convert.ToDecimal(currencyNode.ChildNodes[2].InnerText.Replace(".", ","));
                }

                return ResponseDto<ClientCurrencyGetResponseDto>.Success(response, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ClientCurrencyGetResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        #endregion

        #region Town

        public async Task<ResponseDto<ICollection<ClientDistrictGetAllResponseDto>>> GetAllDistrict()
        {
            try
            {
                var getAllTown = await context.Districts
                    .AsNoTracking()
                    .Where(x => x.Towns.Count > 0)
                    .Select(d => new ClientDistrictGetAllResponseDto
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Towns = d.Towns.Select(t => new ClientDistrictGetAllResponseDtoTown
                        {
                            Name = t.Name
                        }).ToList()
                    })
                    .ToListAsync();

                return ResponseDto<ICollection<ClientDistrictGetAllResponseDto>>.Success(getAllTown, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<ClientDistrictGetAllResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }
        #endregion

        #region WebPages

        public async Task<ResponseDto<ICollection<ClienWebPageGetAllResponseDto>>> GetAllWebPage(ClientGetAllWebPageRequestDto model)
        {
            try
            {
                var webPages = await context.WebPages
                   .AsNoTracking()
                   .Where(wp => wp.GeneralStatusType == GeneralStatusType.Active && wp.Menu.Slug == model.Slug)
                   .Select(wp => new ClienWebPageGetAllResponseDto
                   {
                       Id = wp.Id,
                       Slug = wp.Slug,
                       MetaTitle = wp.Menu.MetaTitle,
                       MetaDescription = wp.Menu.MetaDescription,
                       WebPageDetails = wp.WebPageDetails.Where(wpd => wpd.LanguageCode == model.Language).Take(1).Select(wpd => new ClienWebPageGetAllResponseDtoWebPageDetail
                       {
                           Title = wpd.Title,
                           DescriptionShort = wpd.DescriptionShort
                       }).ToList(),
                       Photos = wp.Photos.OrderBy(wpp => wpp.Line).Take(1).Select(wpp => new ClienWebPageGetAllResponseDtoWebPagePhoto
                       {
                           Title = wpp.Title,
                           Image = wpp.Image,
                           ImgAlt = wpp.ImgAlt,
                           ImgTitle = wpp.ImgTitle
                       }).ToList()

                   })
                   .Skip(model.Pagination.Page * model.Pagination.Size)
                   .Take(model.Pagination.Size)
                   .ToListAsync();


                return ResponseDto<ICollection<ClienWebPageGetAllResponseDto>>.Success(webPages, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<ClienWebPageGetAllResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ClientWebPageGetResponseDto>> GetWebPage(ClientWebPageGetRequestDto model)
        {
            try
            {
                var webPage = await context.WebPages
                    .AsNoTracking()
                    .Where(wp => wp.GeneralStatusType == GeneralStatusType.Active && wp.Slug == model.Slug)
                    .Select(wp => new ClientWebPageGetResponseDto
                    {
                        MetaTitle = wp.MetaTitle,
                        MetaDescription = wp.MetaDescription,
                        WebPageDetails = wp.WebPageDetails.Where(wpd => wpd.LanguageCode == model.Language).Take(1).Select(wpd => new ClientWebPageGetResponseDtoWebPageDetail
                        {
                            Title = wpd.Title,
                            DescriptionShort = wpd.DescriptionShort,
                            DescriptionLong = wpd.DescriptionLong
                        }).ToList(),
                        Photos = wp.Photos.OrderBy(wpp => wpp.Line).Select(wpp => new ClientWebPageGetResponseDtoWebPagePhoto
                        {
                            Title = wpp.Title,
                            Image = wpp.Image,
                            ImgAlt = wpp.ImgAlt,
                            ImgTitle = wpp.ImgTitle
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

                return ResponseDto<ClientWebPageGetResponseDto>.Success(webPage, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ClientWebPageGetResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        #endregion

    }
}
