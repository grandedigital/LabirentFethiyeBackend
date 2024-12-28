using LabirentFethiye.Application.Abstracts.ProjectInterfaces;
using LabirentFethiye.Common.Dtos.GlobalDtos.MailDtos.MailRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientResponseDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.ReservationDtos.ReservationRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.ReservationDtos.ReservationResponseDtos;
using LabirentFethiye.Common.Enums;
using LabirentFethiye.Common.Requests;
using LabirentFethiye.Common.Responses;
using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Infastructure.Abstract.Interfaces;
using LabirentFethiye.Persistence.Contexts;
using LabirentFethiye.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VillaProject.Persistence.Helpers;

namespace LabirentFethiye.Persistence.Concrete.ProjectConcretes
{
    public class ReservationService : IReservationService
    {
        private readonly AppDbContext context;
        private readonly IPriceDateService priceDateService;
        private readonly IMailService mailService;

        public ReservationService(AppDbContext context, IPriceDateService priceDateService, IMailService mailService)
        {
            this.context = context;
            this.priceDateService = priceDateService;
            this.mailService = mailService;
        }
        public async Task<ResponseDto<BaseResponseDto>> Create(ReservationCreateRequestDto model, Guid userId)
        {
            try
            {
                if (model.CheckOut.Date < model.CheckIn.Date) return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Create", Description = "Lütfen tarihleri kontrol ediniz.." } }, 404);
                if (model.CheckIn <= DateTime.MinValue) return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Create CheckIn", Description = "Lütfen tarihleri kontrol ediniz.." } }, 404);
                if (model.CheckOut <= DateTime.MinValue) return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Create CheckOut", Description = "Lütfen tarihleri kontrol ediniz.." } }, 404);
                if (model.VillaId == Guid.Empty || model.RoomId == Guid.Empty) return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Create Villa or Room", Description = "Tesis Id boş olamaz" } }, 400);
                //------

                bool isAvalible = await IsAvailible(new() { CheckIn = model.CheckIn, CheckOut = model.CheckOut, VillaId = model.VillaId, RoomId = model.RoomId });

                if (model.HomeOwner)
                {
                    if (!isAvalible)
                        return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Is Available", Description = "Tesis Belirtilen Tarihler İçin Müsait Değil." } }, 400);
                    //-----

                    Reservation reservation = new()
                    {
                        ReservationNumber = "-",

                        CheckIn = model.CheckIn,
                        CheckOut = model.CheckOut,
                        ReservationChannalType = ReservationChannalType.HomeOwner,
                        ReservationStatusType = ReservationStatusType.Reserved,
                        HomeOwner = model.HomeOwner,

                        VillaId = model.VillaId,
                        RoomId = model.RoomId,

                        GeneralStatusType = GeneralStatusType.Active,
                        CreatedAt = DateTime.Now
                    };

                    await context.Reservations.AddAsync(reservation);
                    var result = await context.SaveChangesAsync();
                    //------


                    return ResponseDto<BaseResponseDto>.Success(new() { Id = reservation.Id }, 200);
                }
                else
                {                    
                    if (model.IdNo == null) return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Create Villa or Room", Description = "IdNo boş olamaz" } }, 400);
                    if (model.Name == null) return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Create Villa or Room", Description = "Name boş olamaz" } }, 400);
                    if (model.Surname == null) return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Create Villa or Room", Description = "Surname boş olamaz" } }, 400);


                    if (!isAvalible)
                        return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Is Available", Description = "Tesis Belirtilen Tarihler İçin Müsait Değil." } }, 400);
                    //-----

                    var prices = await GetReservationPrice(new() { VillaId = model.VillaId, RoomId = model.RoomId, CheckIn = model.CheckIn, CheckOut = model.CheckOut });
                    if (prices.StatusCode != 200) return ResponseDto<BaseResponseDto>.Fail(prices.Errors, 400);
                    //-----

                    // Todo: ExtraPrice,PriceType,IsCleaningPrice,IsDepositPrice işlemleri yapılacak.

                    decimal total = prices.Data.Total;

                    if (model.Discount > 0) total = total - model.Discount;

                    Reservation reservation = new()
                    {
                        ReservationNumber = GeneralFunctions.UniqueNumber(),

                        CheckIn = model.CheckIn,
                        CheckOut = model.CheckOut,
                        ReservationChannalType = ReservationChannalType.Manuel,
                        ReservationStatusType = model.ReservationStatusType,
                        Note = model.Note,
                        IsDepositPrice = model.IsDepositPrice,
                        IsCleaningPrice = model.IsCleaningPrice,
                        HomeOwner = false,

                        Amount = prices.Data.Amount,
                        PriceType = prices.Data.PriceType,
                        ExtraPrice = prices.Data.ExtraPrice,
                        Discount = model.Discount,
                        Total = total,

                        VillaId = model.VillaId,
                        RoomId = model.RoomId,

                        GeneralStatusType = GeneralStatusType.Active,
                        CreatedAt = DateTime.Now,
                        CreatedById = userId,

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

                    // Mail sender
                    if (reservation.ReservationChannalType == ReservationChannalType.Manuel)
                    {
                        Villa villa = null;
                        Room room = null;
                        if (model.VillaId == Guid.Empty || model.VillaId != null)
                        {
                            villa = await context.Villas.Include(x => x.VillaDetails).Where(x => x.Id == model.VillaId).FirstOrDefaultAsync();
                        }
                        else
                        {
                            room = await context.Rooms.Include(x => x.Hotel).ThenInclude(x => x.HotelDetails).Include(x => x.RoomDetails).Where(x => x.Id == model.RoomId).FirstOrDefaultAsync();

                        }
                        ReservationCreateMailRequestDto reservationCreateMailRequestDto = new()
                        {
                            ReservationNumber = reservation.ReservationNumber,
                            CheckIn = reservation.CheckIn,
                            CheckOut = reservation.CheckOut,
                            Note = reservation.Note,
                            PriceType = reservation.PriceType,
                            ReservationChannalType = reservation.ReservationChannalType,
                            ReservationStatusType = reservation.ReservationStatusType,
                            IsDepositPrice = reservation.IsDepositPrice,
                            IsCleaningPrice = reservation.IsCleaningPrice,
                            Villa = villa != null ? new ReservationCreateMailRequestDtoVilla()
                            {
                                Name = reservation.Villa.VillaDetails.FirstOrDefault().Name,
                                Person = reservation.Villa.Person
                            } : null,
                            Room = room != null ? new ReservationCreateMailRequestDtoRoom()
                            {
                                HotelName = reservation.Room.Hotel.HotelDetails.FirstOrDefault().Name,
                                Name = reservation.Room.RoomDetails.FirstOrDefault().Name,
                                Person = reservation.Room.Person
                            } : null,
                            ReservationInfo = new ReservationCreateMailRequestDtoReservationInfos()
                            {
                                Name = reservation.ReservationInfos.FirstOrDefault().Name,
                                Surname = reservation.ReservationInfos.FirstOrDefault().Surname,
                                Email = reservation.ReservationInfos.FirstOrDefault().Email,
                                IdNo = reservation.ReservationInfos.FirstOrDefault().IdNo,
                                Phone = reservation.ReservationInfos.FirstOrDefault().Phone
                            },
                            ExtraPrice = reservation.ExtraPrice,
                            Amount = reservation.Amount,
                            Discount = reservation.Discount,
                            Total = reservation.Total,
                        };

                        await mailService.ReservationCreateSendMailAsync(reservationCreateMailRequestDto);
                    }

                    return ResponseDto<BaseResponseDto>.Success(new() { Id = reservation.Id }, 200);
                }
            }
            catch (Exception ex) { return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500); }
        }

        public async Task<ResponseDto<BaseResponseDto>> Delete(Guid Id, Guid userId)
        {
            try
            {
                var getReservation = await context.Reservations.FirstOrDefaultAsync(x => x.Id == Id);
                if (getReservation != null)
                {
                    getReservation.UpdatedAt = DateTime.Now;
                    getReservation.UpdatedById = userId;
                    getReservation.GeneralStatusType = GeneralStatusType.Deleted;

                    context.Reservations.Update(getReservation);
                    await context.SaveChangesAsync();
                    return ResponseDto<BaseResponseDto>.Success(new() { Id = getReservation.Id }, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Reservation Not Found", Description = "Rezervasyon Bulunamadı.." } }, 400);
            }
            catch (Exception ex) { return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500); }
        }

        public async Task<ResponseDto<ReservationGetResponseDto>> Get(Guid Id)
        {
            try
            {
                var getReservation = await context.Reservations
                    .Where(x => x.Id == Id)
                    .Include(x => x.ReservationInfos.Where(x => x.GeneralStatusType == GeneralStatusType.Active))
                    .Include(x => x.ReservationItems)
                    .Include(x => x.Payments.Where(x => x.GeneralStatusType == GeneralStatusType.Active))
                    .Include(x => x.Villa).ThenInclude(x => x.VillaDetails)
                    .Include(x => x.Room).ThenInclude(x => x.RoomDetails)
                    .Select(reservation => new ReservationGetResponseDto()
                    {
                        Id = reservation.Id,
                        ReservationNumber = reservation.ReservationNumber,
                        ReservationChannalType = reservation.ReservationChannalType,
                        ReservationStatusType = reservation.ReservationStatusType,
                        Note = reservation.Note,
                        CheckIn = reservation.CheckIn,
                        CheckOut = reservation.CheckOut,
                        IsDepositPrice = reservation.IsDepositPrice,
                        IsCleaningPrice = reservation.IsCleaningPrice,
                        HomeOwner = reservation.HomeOwner,
                        Amount = reservation.Amount,
                        ExtraPrice = reservation.ExtraPrice,
                        Discount = reservation.Discount,
                        Total = reservation.Total,
                        PriceType = reservation.PriceType,
                        RoomId = reservation.RoomId,
                        VillaId = reservation.VillaId,

                        ReservationInfos = reservation.ReservationInfos.Select(reservationInfo => new ReservationGetResponseDtoReservationInfo()
                        {
                            Name = reservationInfo.Name,
                            Surname = reservationInfo.Surname,
                            Email = reservationInfo.Email,
                            IdNo = reservationInfo.IdNo,
                            Owner = reservationInfo.Owner,
                            PeopleType = reservationInfo.PeopleType,
                            Phone = reservationInfo.Phone
                        }).ToList(),
                        ReservationItems = reservation.ReservationItems.Select(reservationItem => new ReservationGetResponseDtoReservationItem()
                        {
                            Day = reservationItem.Day,
                            Price = reservationItem.Price
                        }).ToList(),
                        Payments = reservation.Payments.Select(reservationPayment => new ReservationGetResponseDtoPayments()
                        {
                            Amount = reservationPayment.Amount,
                            InOrOut = reservationPayment.InOrOut,
                            PriceType = reservationPayment.PriceType

                        }).ToList(),
                        Villa = new()
                        {
                            Name = reservation.Villa.VillaDetails.FirstOrDefault().Name
                        },
                        Room = new()
                        {
                            Name = reservation.Room.RoomDetails.FirstOrDefault().Name
                        }
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                return ResponseDto<ReservationGetResponseDto>.Success(getReservation, 200);
            }
            catch (Exception ex) { return ResponseDto<ReservationGetResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500); }
        }

        public async Task<ResponseDto<ICollection<ReservationGetAllResponseDto>>> GetAll(GetAllReservationRequestDto model)
        {
            try
            {
                // Todo: Bu servis yenilendi. Çok detaylı Test yapılacak

                var query = context.Reservations
                    .AsNoTracking()
                    .AsQueryable();


                if (!model.HomeOwner && model.AgencyOwner)
                    query = query.Where(x => x.HomeOwner == false);
                else if (model.HomeOwner && !model.AgencyOwner)
                    query = query.Where(x => x.HomeOwner == true); ;


                if (!String.IsNullOrEmpty(model.CustomerSearchName))
                    query = query.Where(x => x.ReservationInfos.Any(x => x.Owner == true && x.Name.Contains(model.CustomerSearchName)));



                if (model.OrderByCustomerName != null)
                {
                    // +OrderByCustomerName
                    if (model.OrderByCustomerName == true)
                        query = query
                            .Select(x => new { Item = x, FirstReservationName = x.ReservationInfos.FirstOrDefault().Name })
                            .OrderBy(x => x.FirstReservationName)
                            .Select(x => x.Item);
                    else
                        query = query
                            .Select(x => new { Item = x, FirstReservationName = x.ReservationInfos.FirstOrDefault().Name })
                            .OrderByDescending(x => x.FirstReservationName)
                            .Select(x => x.Item);
                }
                else if (model.OrderByReservationStatus != null)
                {
                    // +OrderByReservationStatus                        
                    if (model.OrderByReservationStatus == true)
                        query = query.OrderBy(x => x.ReservationStatusType);
                    else
                        query = query.OrderByDescending(x => x.ReservationStatusType);
                }
                else if (model.OrderByCheckIn != null)
                {
                    // +OrderByCheckIn
                    if (model.OrderByCheckIn == true)
                        query = query.OrderBy(x => x.CheckIn);
                    else
                        query = query.OrderByDescending(x => x.CheckIn);
                }
                else if (model.OrderByCheckOut != null)
                {
                    // +OrderByCheckOut
                    if (model.OrderByCheckOut == true)
                        query = query.OrderBy(x => x.CheckOut);
                    else
                        query = query.OrderByDescending(x => x.CheckOut);
                }
                else if (model.OrderByPrice != null)
                {
                    // +OrderByPrice
                    if (model.OrderByPrice == true)
                        query = query.OrderBy(x => x.Total);
                    else
                        query = query.OrderByDescending(x => x.Total);
                }
                else
                {
                    // +CreatedAt
                    query = query.OrderByDescending(x => x.CreatedAt);
                }


                int TotalCount = query.Count();
                query = query.Skip(model.Pagination.Page * model.Pagination.Size).Take(model.Pagination.Size);


                var getAllReservation = await query
                      .Select(reservation => new ReservationGetAllResponseDto()
                      {
                          Id = reservation.Id,
                          ReservationNumber = reservation.ReservationNumber,
                          ReservationChannalType = reservation.ReservationChannalType,
                          ReservationStatusType = reservation.ReservationStatusType,
                          Note = reservation.Note,
                          CheckIn = reservation.CheckIn,
                          CheckOut = reservation.CheckOut,
                          IsDepositPrice = reservation.IsDepositPrice,
                          IsCleaningPrice = reservation.IsCleaningPrice,
                          HomeOwner = reservation.HomeOwner,
                          Amount = reservation.Amount,
                          ExtraPrice = reservation.ExtraPrice,
                          Discount = reservation.Discount,
                          Total = reservation.Total,
                          PriceType = reservation.PriceType,
                          RoomId = reservation.RoomId,
                          VillaId = reservation.VillaId,

                          ReservationInfos = reservation.ReservationInfos.Select(reservationInfo => new ReservationGetAllResponseDtoReservationInfo()
                          {
                              Name = reservationInfo.Name,
                              Surname = reservationInfo.Surname
                          }).ToList(),
                          ReservationItems = reservation.ReservationItems.Select(reservationItem => new ReservationGetAllResponseDtoReservationItem()
                          {
                              Day = reservationItem.Day,
                              Price = reservationItem.Price
                          }).ToList(),
                          Villa = new()
                          {
                              Name = reservation.Villa.VillaDetails.FirstOrDefault().Name
                          },
                          Room = new()
                          {
                              Name = reservation.Room.RoomDetails.FirstOrDefault().Name
                          }
                      })
                      .ToListAsync();

                PageInfo pageInfo = GeneralFunctions.PageInfoHelper(Page: model.Pagination.Page, Size: model.Pagination.Size, TotalCount: TotalCount);

                return ResponseDto<ICollection<ReservationGetAllResponseDto>>.Success(getAllReservation, 200, pageInfo);

                #region Eski
                #region Delgetes
                //Expression<Func<Reservation, string>> orderByString = null;
                //Expression<Func<Reservation, string>> orderByDescendingString = null;

                //Expression<Func<Reservation, decimal>> orderByDecimal = null;
                //Expression<Func<Reservation, decimal>> orderByDescendingDecimal = null;

                //Expression<Func<Reservation, ReservationStatusType>> orderByInt = null;
                //Expression<Func<Reservation, ReservationStatusType>> orderByDescendingInt = null;

                //Expression<Func<Reservation, DateTime>> orderByDateTime = null;
                //Expression<Func<Reservation, DateTime>> orderByDescendingDateTime = null;

                //var predicate = PredicateBuilder.False<Reservation>();

                //if (!model.HomeOwner && model.AgencyOwner) predicate = predicate.And(p => p.HomeOwner == false);
                //else if (model.HomeOwner && !model.AgencyOwner) predicate = predicate.And(p => p.HomeOwner == true);

                //if (!String.IsNullOrEmpty(model.CustomerSearchName)) predicate = predicate.And(p => p.ReservationInfos.Any(x => x.Owner == true && x.Name.Contains(model.CustomerSearchName)));

                #endregion

                #region Filterings

                //if (model.OrderByCustomerName != null)
                //{
                //    // +OrderByCustomerName

                //    if (model.OrderByCustomerName == true)
                //        orderByString = e => e.ReservationInfos.FirstOrDefault().Name;
                //    else
                //        orderByDescendingString = e => e.ReservationInfos.FirstOrDefault().Name;
                //}
                //else if (model.OrderByReservationStatus != null)
                //{
                //    // OrderByReservationStatus                        

                //    if (model.OrderByReservationStatus == true)
                //        orderByInt = e => e.ReservationStatusType;
                //    else
                //        orderByDescendingInt = e => e.ReservationStatusType;
                //}
                //else if (model.OrderByCheckIn != null)
                //{
                //    // +OrderByCheckIn                        

                //    if (model.OrderByCheckIn == true)
                //        orderByDateTime = e => e.CheckIn;
                //    else
                //        orderByDescendingDateTime = e => e.CheckIn;
                //}
                //else if (model.OrderByCheckOut != null)
                //{
                //    // +OrderByCheckOut
                //    if (model.OrderByCheckOut == true)
                //        orderByDateTime = e => e.CheckOut;
                //    else
                //        orderByDescendingDateTime = e => e.CheckOut;
                //}
                //else if (model.OrderByPrice != null)
                //{
                //    // +OrderByPrice
                //    if (model.OrderByPrice == true)
                //        orderByDecimal = e => e.Total;
                //    else
                //        orderByDescendingDecimal = e => e.Total;
                //}
                //else
                //{
                //    // +CreatedAt
                //    orderByDescendingDateTime = e => e.CreatedAt;
                //}

                #endregion

                #region Queries

                //var result = context.Reservations
                //    .AsQueryable()
                //    .AsNoTracking()
                //    .Where(x => x.GeneralStatusType == GeneralStatusType.Active)
                //    .Where(predicate);


                //// Total Count Query
                //int TotalCount = await result.CountAsync();

                //// Listing
                //if (orderByString != null) result = result.OrderBy(orderByString);
                //else if (orderByInt != null) result = result.OrderBy(orderByInt);
                //else if (orderByDecimal != null) result = result.OrderBy(orderByDecimal);
                //else if (orderByDateTime != null) result = result.OrderBy(orderByDateTime);
                //else if (orderByDescendingString != null) result = result.OrderBy(orderByDescendingString);
                //else if (orderByDescendingInt != null) result = result.OrderBy(orderByDescendingInt);
                //else if (orderByDescendingDecimal != null) result = result.OrderBy(orderByDescendingDecimal);
                //else result = result.OrderByDescending(orderByDescendingDateTime);

                //// pagination
                //result = result.Skip(pagination.Page * pagination.Size).Take(pagination.Size);

                //var getAllReservation = await result
                //      .Select(reservation => new ReservationGetAllResponseDto()
                //      {
                //          Id = reservation.Id,
                //          ReservationNumber = reservation.ReservationNumber,
                //          ReservationChannalType = reservation.ReservationChannalType,
                //          ReservationStatusType = reservation.ReservationStatusType,
                //          Note = reservation.Note,
                //          CheckIn = reservation.CheckIn,
                //          CheckOut = reservation.CheckOut,
                //          IsDepositPrice = reservation.IsDepositPrice,
                //          IsCleaningPrice = reservation.IsCleaningPrice,
                //          HomeOwner = reservation.HomeOwner,
                //          Amount = reservation.Amount,
                //          ExtraPrice = reservation.ExtraPrice,
                //          Discount = reservation.Discount,
                //          Total = reservation.Total,
                //          PriceType = reservation.PriceType,
                //          RoomId = reservation.RoomId,
                //          VillaId = reservation.VillaId,

                //          ReservationInfos = reservation.ReservationInfos.Select(reservationInfo => new ReservationGetAllResponseDtoReservationInfo()
                //          {
                //              Name = reservationInfo.Name,
                //              Surname = reservationInfo.Surname
                //          }).ToList(),
                //          ReservationItems = reservation.ReservationItems.Select(reservationItem => new ReservationGetAllResponseDtoReservationItem()
                //          {
                //              Day = reservationItem.Day,
                //              Price = reservationItem.Price
                //          }).ToList(),
                //          Villa = new()
                //          {
                //              Name = reservation.Villa.VillaDetails.FirstOrDefault().Name
                //          },
                //          Room = new()
                //          {
                //              Name = reservation.Room.RoomDetails.FirstOrDefault().Name
                //          }
                //      })
                //      .ToListAsync();

                //PageInfo pageInfo = GeneralFunctions.PageInfoHelper(Page: model.Pagination.Page, Size: model.Pagination.Size, TotalCount: TotalCount);

                //return ResponseDto<ICollection<ReservationGetAllResponseDto>>.Success(getAllReservation, 200, pageInfo);
                #endregion
                #endregion
                #region Eski

                ////                List<Reservation> getAllReservation = new List<Reservation>();
                ////----------------



                ////Expression<Func<Reservation, bool>> predicate = e => e.HomeOwner == true;
                ////getAllReservation = await _context.Reservations.Where(predicate).ToListAsync();


                ////Expression<Func<Reservation, string>> orderBy = e => e.ReservationInfos.FirstOrDefault().Name;

                //Expression<Func<Reservation, string>> orderByString = null;
                //Expression<Func<Reservation, string>> orderByDescendingString = null;

                //Expression<Func<Reservation, decimal>> orderByDecimal = null;
                //Expression<Func<Reservation, decimal>> orderByDescendingDecimal = null;

                //Expression<Func<Reservation, ReservationStatusType>> orderByInt = null;
                //Expression<Func<Reservation, ReservationStatusType>> orderByDescendingInt = null;

                //Expression<Func<Reservation, DateTime>> orderByDateTime = null;
                //Expression<Func<Reservation, DateTime>> orderByDescendingDateTime = null;


                //var predicate = PredicateBuilder.False<Reservation>();
                ////predicate = predicate.Or(p => p.ReservationInfos.Any(x => x.Owner));
                ////predicate = predicate.Or(p => 1 == 1);
                //////predicate = predicate.Or(p => p.Villa.CompanyId == CompanyId || p.Room.Hotel.CompanyId == CompanyId);

                //int TotalCount = 0;


                //if (!model.HomeOwner && model.AgencyOwner)
                //    predicate = predicate.And(p => p.HomeOwner == false);
                //else if (model.HomeOwner && !model.AgencyOwner)
                //    predicate = predicate.And(p => p.HomeOwner == true);
                ////-----

                //if (!String.IsNullOrEmpty(model.CustomerSearchName))
                //{
                //    predicate = predicate.And(p => p.ReservationInfos.Any(x => x.Owner == true && x.Name.Contains(model.CustomerSearchName)));
                //    //--

                //    if (model.OrderByCustomerName != null)
                //    {
                //        // +OrderByCustomerName

                //        if (model.OrderByCustomerName == true)
                //            orderByString = e => e.ReservationInfos.FirstOrDefault().Name;
                //        else
                //            orderByDescendingString = e => e.ReservationInfos.FirstOrDefault().Name;
                //    }
                //    else if (model.OrderByReservationStatus != null)
                //    {
                //        // OrderByReservationStatus                        

                //        if (model.OrderByReservationStatus == true)
                //            orderByInt = e => e.ReservationStatusType;
                //        else
                //            orderByDescendingInt = e => e.ReservationStatusType;
                //    }
                //    else if (model.OrderByCheckIn != null)
                //    {
                //        // +OrderByCheckIn                        

                //        if (model.OrderByCheckIn == true)
                //            orderByDateTime = e => e.CheckIn;
                //        else
                //            orderByDescendingDateTime = e => e.CheckIn;
                //    }
                //    else if (model.OrderByCheckOut != null)
                //    {
                //        // +OrderByCheckOut
                //        if (model.OrderByCheckOut == true)
                //            orderByDateTime = e => e.CheckOut;
                //        else
                //            orderByDescendingDateTime = e => e.CheckOut;
                //    }
                //    else if (model.OrderByPrice != null)
                //    {
                //        // +OrderByPrice
                //        if (model.OrderByPrice == true)
                //            orderByDecimal = e => e.Total;
                //        else
                //            orderByDescendingDecimal = e => e.Total;
                //    }
                //    else
                //    {
                //        // +CreatedAt
                //        orderByDescendingDateTime = e => e.CreatedAt;
                //    }
                //}
                //else
                //{
                //    if (model.OrderByCustomerName != null)
                //    {
                //        // +OrderByCustomerName

                //        if (model.OrderByCustomerName == true)
                //            orderByString = e => e.ReservationInfos.FirstOrDefault().Name;
                //        else
                //            orderByDescendingString = e => e.ReservationInfos.FirstOrDefault().Name;
                //    }
                //    else if (model.OrderByReservationStatus != null)
                //    {
                //        // OrderByReservationStatus                        

                //        if (model.OrderByReservationStatus == true)
                //            orderByInt = e => e.ReservationStatusType;
                //        else
                //            orderByDescendingInt = e => e.ReservationStatusType;
                //    }
                //    else if (model.OrderByCheckIn != null)
                //    {
                //        // +OrderByCheckIn                        

                //        if (model.OrderByCheckIn == true)
                //            orderByDateTime = e => e.CheckIn;
                //        else
                //            orderByDescendingDateTime = e => e.CheckIn;
                //    }
                //    else if (model.OrderByCheckOut != null)
                //    {
                //        // +OrderByCheckOut
                //        if (model.OrderByCheckOut == true)
                //            orderByDateTime = e => e.CheckOut;
                //        else
                //            orderByDescendingDateTime = e => e.CheckOut;
                //    }
                //    else if (model.OrderByPrice != null)
                //    {
                //        // +OrderByPrice
                //        if (model.OrderByPrice == true)
                //            orderByDecimal = e => e.Total;
                //        else
                //            orderByDescendingDecimal = e => e.Total;
                //    }
                //    else
                //    {
                //        // +CreatedAt
                //        orderByDescendingDateTime = e => e.CreatedAt;
                //    }
                //}
                ////-----


                //IQueryable<Reservation> res;

                //#region Queries

                //if (orderByString != null)
                //    res = context.Reservations
                //    .AsQueryable()
                //    .AsNoTracking()
                //          .Where(x => x.GeneralStatusType == GeneralStatusType.Active)
                //          .Where(predicate)
                //          .OrderBy(orderByString)
                //          .Include(x => x.Villa).ThenInclude(x => x.VillaDetails.Take(1))
                //          .Include(x => x.Room).ThenInclude(x => x.RoomDetails.Take(1))
                //          .Include(x => x.ReservationInfos)
                //          .Skip(pagination.Page * pagination.Size)
                //          .Take(pagination.Size);
                //else if (orderByInt != null)
                //    res = context.Reservations
                //    .AsQueryable()
                //    .AsNoTracking()
                //        .Where(x => x.GeneralStatusType == GeneralStatusType.Active)
                //        .Where(predicate)
                //        .OrderBy(orderByInt)
                //        .Include(x => x.Villa).ThenInclude(x => x.VillaDetails.Take(1))
                //        .Include(x => x.Room).ThenInclude(x => x.RoomDetails.Take(1))
                //        .Include(x => x.ReservationInfos)
                //        .Skip(pagination.Page * pagination.Size)
                //        .Take(pagination.Size);
                //else if (orderByDecimal != null)
                //    res = context.Reservations
                //    .AsQueryable()
                //    .AsNoTracking()
                //        .Where(x => x.GeneralStatusType == GeneralStatusType.Active)
                //        .Where(predicate)
                //        .OrderBy(orderByDecimal)
                //        .Include(x => x.Villa).ThenInclude(x => x.VillaDetails.Take(1))
                //        .Include(x => x.Room).ThenInclude(x => x.RoomDetails.Take(1))
                //        .Include(x => x.ReservationInfos)
                //        .Skip(pagination.Page * pagination.Size)
                //        .Take(pagination.Size);
                //else if (orderByDateTime != null)
                //    res = context.Reservations
                //    .AsQueryable()
                //    .AsNoTracking()
                //        .Where(x => x.GeneralStatusType == GeneralStatusType.Active)
                //        .Where(predicate)
                //        .OrderBy(orderByDateTime)
                //        .Include(x => x.Villa).ThenInclude(x => x.VillaDetails.Take(1))
                //        .Include(x => x.Room).ThenInclude(x => x.RoomDetails.Take(1))
                //        .Include(x => x.ReservationInfos)
                //        .Skip(pagination.Page * pagination.Size)
                //        .Take(pagination.Size);
                //else if (orderByDescendingString != null)
                //    res = context.Reservations
                //    .AsQueryable()
                //    .AsNoTracking()
                //        .Where(x => x.GeneralStatusType == GeneralStatusType.Active)
                //        .Where(predicate)
                //        .OrderByDescending(orderByDescendingString)
                //        .Include(x => x.Villa).ThenInclude(x => x.VillaDetails.Take(1))
                //        .Include(x => x.Room).ThenInclude(x => x.RoomDetails.Take(1))
                //        .Include(x => x.ReservationInfos)
                //        .Skip(pagination.Page * pagination.Size)
                //        .Take(pagination.Size);
                //else if (orderByDescendingInt != null)
                //    res = context.Reservations
                //    .AsQueryable()
                //    .AsNoTracking()
                //        .Where(x => x.GeneralStatusType == GeneralStatusType.Active)
                //        .Where(predicate)
                //        .OrderByDescending(orderByDescendingInt)
                //        .Include(x => x.Villa).ThenInclude(x => x.VillaDetails.Take(1))
                //        .Include(x => x.Room).ThenInclude(x => x.RoomDetails.Take(1))
                //        .Include(x => x.ReservationInfos)
                //        .Skip(pagination.Page * pagination.Size)
                //        .Take(pagination.Size);
                //else if (orderByDescendingDecimal != null)
                //    res = context.Reservations
                //    .AsQueryable()
                //    .AsNoTracking()
                //        .Where(x => x.GeneralStatusType == GeneralStatusType.Active)
                //        .Where(predicate)
                //        .OrderByDescending(orderByDescendingDecimal)
                //        .Include(x => x.Villa).ThenInclude(x => x.VillaDetails.Take(1))
                //        .Include(x => x.Room).ThenInclude(x => x.RoomDetails.Take(1))
                //        .Include(x => x.ReservationInfos)
                //        .Skip(pagination.Page * pagination.Size)
                //        .Take(pagination.Size);
                //else
                //    res = context.Reservations
                //    .AsQueryable()
                //    .AsNoTracking()
                //        .Where(x => x.GeneralStatusType == GeneralStatusType.Active)
                //        .Where(predicate)
                //        .OrderByDescending(orderByDescendingDateTime)
                //        .Include(x => x.Villa).ThenInclude(x => x.VillaDetails.Take(1))
                //        .Include(x => x.Room).ThenInclude(x => x.RoomDetails.Take(1))
                //        .Include(x => x.ReservationInfos)
                //        .Skip(pagination.Page * pagination.Size)
                //        .Take(pagination.Size);
                //#endregion




                //var getAllReservation = await res
                //      .Select(reservation => new ReservationGetAllResponseDto()
                //      {
                //          Id = reservation.Id,
                //          ReservationNumber = reservation.ReservationNumber,
                //          ReservationChannalType = reservation.ReservationChannalType,
                //          ReservationStatusType = reservation.ReservationStatusType,
                //          Note = reservation.Note,
                //          CheckIn = reservation.CheckIn,
                //          CheckOut = reservation.CheckOut,
                //          IsDepositPrice = reservation.IsDepositPrice,
                //          IsCleaningPrice = reservation.IsCleaningPrice,
                //          HomeOwner = reservation.HomeOwner,
                //          Amount = reservation.Amount,
                //          ExtraPrice = reservation.ExtraPrice,
                //          Discount = reservation.Discount,
                //          Total = reservation.Total,
                //          PriceType = reservation.PriceType,
                //          RoomId = reservation.RoomId,
                //          VillaId = reservation.VillaId,

                //          ReservationInfos = reservation.ReservationInfos.Select(reservationInfo => new ReservationGetAllResponseDtoReservationInfo()
                //          {
                //              Name = reservationInfo.Name,
                //              Surname = reservationInfo.Surname
                //          }).ToList(),
                //          ReservationItems = reservation.ReservationItems.Select(reservationItem => new ReservationGetAllResponseDtoReservationItem()
                //          {
                //              Day = reservationItem.Day,
                //              Price = reservationItem.Price
                //          }).ToList(),
                //          Villa = new()
                //          {
                //              Name = reservation.Villa.VillaDetails.FirstOrDefault().Name
                //          },
                //          Room = new()
                //          {
                //              Name = reservation.Room.RoomDetails.FirstOrDefault().Name
                //          }
                //      })
                //      .ToListAsync();




                //PageInfo pageInfo = new()
                //{
                //    TotalCount = await context.Reservations.Where(predicate).CountAsync()
                //};


                //return ResponseDto<ICollection<ReservationGetAllResponseDto>>.Success(getAllReservation, 200, pageInfo);


                //#region Eski
                ////List<Reservation> getAllReservation = new();

                ////if (model.VillaId != null)
                ////{
                ////    getAllReservation = await _context.Reservations
                ////    .Where(x => x.VillaId == model.VillaId)
                ////    .Include(x => x.Villa).ThenInclude(x => x.VillaDetails)
                ////    .Include(x => x.ReservationInfos)
                ////    .Skip(pagination.Page * pagination.Size)
                ////    .Take(pagination.Size)
                ////    .AsNoTracking()
                ////    .ToListAsync();

                ////    return ResponseDto<ICollection<ReservationGetAllResponseDto>>.Success(_mapper.Map<ICollection<ReservationGetAllResponseDto>>(getAllReservation), 200, await _context.Reservations.Where(x => x.VillaId == model.VillaId).CountAsync());
                ////}
                ////else if (model.RoomId != null)
                ////{
                ////    getAllReservation = await _context.Reservations
                ////     .Where(x => x.RoomId == model.RoomId)
                ////     .Include(x => x.Room).ThenInclude(x => x.RoomDetails)
                ////     .Include(x => x.ReservationInfos)
                ////     .Skip(pagination.Page * pagination.Size)
                ////     .Take(pagination.Size)
                ////     .AsNoTracking()
                ////     .ToListAsync();

                ////    return ResponseDto<ICollection<ReservationGetAllResponseDto>>.Success(_mapper.Map<ICollection<ReservationGetAllResponseDto>>(getAllReservation), 200, await _context.Reservations.Where(x => x.RoomId == model.RoomId).CountAsync());
                ////}
                ////else
                ////{
                ////    getAllReservation = await _context.Reservations
                ////        .Include(x => x.Villa).ThenInclude(x => x.VillaDetails)
                ////        .Include(x => x.Room).ThenInclude(x => x.RoomDetails)
                ////        .Include(x => x.ReservationInfos)
                ////        .Skip(pagination.Page * pagination.Size)
                ////        .Take(pagination.Size)
                ////        .AsNoTracking()
                ////        .ToListAsync();

                ////    return ResponseDto<ICollection<ReservationGetAllResponseDto>>.Success(_mapper.Map<ICollection<ReservationGetAllResponseDto>>(getAllReservation), 200, await _context.Reservations.CountAsync());
                ////}
                //#endregion
                //#region Eski1

                ////if (orderByString != null)
                ////    getAllReservation = await context.Reservations
                ////          .Where(x => x.GeneralStatusType == GeneralStatusType.Active)
                ////          .Where(predicate)
                ////          .OrderBy(orderByString)
                ////          .Include(x => x.Villa).ThenInclude(x => x.VillaDetails.Take(1))
                ////          .Include(x => x.Room).ThenInclude(x => x.RoomDetails.Take(1))
                ////          .Include(x => x.ReservationInfos)
                ////          .Skip(pagination.Page * pagination.Size)
                ////          .Take(pagination.Size)
                ////          .AsNoTracking()
                ////          .ToListAsync();
                ////else if (orderByInt != null)
                ////    getAllReservation = await context.Reservations
                ////        .Where(x => x.GeneralStatusType == GeneralStatusType.Active)
                ////        .Where(predicate)
                ////        .OrderBy(orderByInt)
                ////        .Include(x => x.Villa).ThenInclude(x => x.VillaDetails.Take(1))
                ////        .Include(x => x.Room).ThenInclude(x => x.RoomDetails.Take(1))
                ////        .Include(x => x.ReservationInfos)
                ////        .Skip(pagination.Page * pagination.Size)
                ////        .Take(pagination.Size)
                ////        .AsNoTracking()
                ////        .ToListAsync();
                ////else if (orderByDecimal != null)
                ////    getAllReservation = await context.Reservations
                ////        .Where(x => x.GeneralStatusType == GeneralStatusType.Active)
                ////        .Where(predicate)
                ////        .OrderBy(orderByDecimal)
                ////        .Include(x => x.Villa).ThenInclude(x => x.VillaDetails.Take(1))
                ////        .Include(x => x.Room).ThenInclude(x => x.RoomDetails.Take(1))
                ////        .Include(x => x.ReservationInfos)
                ////        .Skip(pagination.Page * pagination.Size)
                ////        .Take(pagination.Size)
                ////        .AsNoTracking()
                ////        .ToListAsync();
                ////else if (orderByDateTime != null)
                ////    getAllReservation = await context.Reservations
                ////        .Where(x => x.GeneralStatusType == GeneralStatusType.Active)
                ////        .Where(predicate)
                ////        .OrderBy(orderByDateTime)
                ////        .Include(x => x.Villa).ThenInclude(x => x.VillaDetails.Take(1))
                ////        .Include(x => x.Room).ThenInclude(x => x.RoomDetails.Take(1))
                ////        .Include(x => x.ReservationInfos)
                ////        .Skip(pagination.Page * pagination.Size)
                ////        .Take(pagination.Size)
                ////        .AsNoTracking()
                ////        .ToListAsync();
                ////else if (orderByDescendingString != null)
                ////    getAllReservation = await context.Reservations
                ////        .Where(x => x.GeneralStatusType == GeneralStatusType.Active)
                ////        .Where(predicate)
                ////        .OrderByDescending(orderByDescendingString)
                ////        .Include(x => x.Villa).ThenInclude(x => x.VillaDetails.Take(1))
                ////        .Include(x => x.Room).ThenInclude(x => x.RoomDetails.Take(1))
                ////        .Include(x => x.ReservationInfos)
                ////        .Skip(pagination.Page * pagination.Size)
                ////        .Take(pagination.Size)
                ////        .AsNoTracking()
                ////        .ToListAsync();
                ////else if (orderByDescendingInt != null)
                ////    getAllReservation = await context.Reservations
                ////        .Where(x => x.GeneralStatusType == GeneralStatusType.Active)
                ////        .Where(predicate)
                ////        .OrderByDescending(orderByDescendingInt)
                ////        .Include(x => x.Villa).ThenInclude(x => x.VillaDetails.Take(1))
                ////        .Include(x => x.Room).ThenInclude(x => x.RoomDetails.Take(1))
                ////        .Include(x => x.ReservationInfos)
                ////        .Skip(pagination.Page * pagination.Size)
                ////        .Take(pagination.Size)
                ////        .AsNoTracking()
                ////        .ToListAsync();
                ////else if (orderByDescendingDecimal != null)
                ////    getAllReservation = await context.Reservations
                ////        .Where(x => x.GeneralStatusType == GeneralStatusType.Active)
                ////        .Where(predicate)
                ////        .OrderByDescending(orderByDescendingDecimal)
                ////        .Include(x => x.Villa).ThenInclude(x => x.VillaDetails.Take(1))
                ////        .Include(x => x.Room).ThenInclude(x => x.RoomDetails.Take(1))
                ////        .Include(x => x.ReservationInfos)
                ////        .Skip(pagination.Page * pagination.Size)
                ////        .Take(pagination.Size)
                ////        .AsNoTracking()
                ////        .ToListAsync();
                ////else
                ////    getAllReservation = await context.Reservations
                ////        .Where(x => x.GeneralStatusType == GeneralStatusType.Active)
                ////        .Where(predicate)
                ////        .OrderByDescending(orderByDescendingDateTime)
                ////        .Include(x => x.Villa).ThenInclude(x => x.VillaDetails.Take(1))
                ////        .Include(x => x.Room).ThenInclude(x => x.RoomDetails.Take(1))
                ////        .Include(x => x.ReservationInfos)
                ////        .Skip(pagination.Page * pagination.Size)
                ////        .Take(pagination.Size)
                ////        .AsNoTracking()
                ////        .ToListAsync(); 
                //#endregion

                #endregion
            }
            catch (Exception ex) { return ResponseDto<ICollection<ReservationGetAllResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500); }
        }

        public async Task<ResponseDto<ICollection<ReservationGetAllResponseDto>>> GetAllForRoom(GetAllReservationRequestDto model)
        {
            try
            {
                // Todo: Bu servis yenilendi. Çok detaylı Test yapılacak
                var query = context.Reservations
                      .AsNoTracking()
                      .AsQueryable()
                      .Where(x => x.RoomId == model.RoomId);


                if (!model.HomeOwner && model.AgencyOwner)
                    query = query.Where(x => x.HomeOwner == false);
                else if (model.HomeOwner && !model.AgencyOwner)
                    query = query.Where(x => x.HomeOwner == true); ;


                if (!String.IsNullOrEmpty(model.CustomerSearchName))
                    query = query.Where(x => x.ReservationInfos.Any(x => x.Owner == true && x.Name.Contains(model.CustomerSearchName)));



                if (model.OrderByCustomerName != null)
                {
                    // +OrderByCustomerName
                    if (model.OrderByCustomerName == true)
                        query = query
                            .Select(x => new { Item = x, FirstReservationName = x.ReservationInfos.FirstOrDefault().Name })
                            .OrderBy(x => x.FirstReservationName)
                            .Select(x => x.Item);
                    else
                        query = query
                            .Select(x => new { Item = x, FirstReservationName = x.ReservationInfos.FirstOrDefault().Name })
                            .OrderByDescending(x => x.FirstReservationName)
                            .Select(x => x.Item);
                }
                else if (model.OrderByReservationStatus != null)
                {
                    // +OrderByReservationStatus                        
                    if (model.OrderByReservationStatus == true)
                        query = query.OrderBy(x => x.ReservationStatusType);
                    else
                        query = query.OrderByDescending(x => x.ReservationStatusType);
                }
                else if (model.OrderByCheckIn != null)
                {
                    // +OrderByCheckIn
                    if (model.OrderByCheckIn == true)
                        query = query.OrderBy(x => x.CheckIn);
                    else
                        query = query.OrderByDescending(x => x.CheckIn);
                }
                else if (model.OrderByCheckOut != null)
                {
                    // +OrderByCheckOut
                    if (model.OrderByCheckOut == true)
                        query = query.OrderBy(x => x.CheckOut);
                    else
                        query = query.OrderByDescending(x => x.CheckOut);
                }
                else if (model.OrderByPrice != null)
                {
                    // +OrderByPrice
                    if (model.OrderByPrice == true)
                        query = query.OrderBy(x => x.Total);
                    else
                        query = query.OrderByDescending(x => x.Total);
                }
                else
                {
                    // +CreatedAt
                    query = query.OrderByDescending(x => x.CreatedAt);
                }


                int TotalCount = query.Count();
                query = query.Skip(model.Pagination.Page * model.Pagination.Size).Take(model.Pagination.Size);


                var getAllReservation = await query
                      .Select(reservation => new ReservationGetAllResponseDto()
                      {
                          Id = reservation.Id,
                          ReservationNumber = reservation.ReservationNumber,
                          ReservationChannalType = reservation.ReservationChannalType,
                          ReservationStatusType = reservation.ReservationStatusType,
                          Note = reservation.Note,
                          CheckIn = reservation.CheckIn,
                          CheckOut = reservation.CheckOut,
                          IsDepositPrice = reservation.IsDepositPrice,
                          IsCleaningPrice = reservation.IsCleaningPrice,
                          HomeOwner = reservation.HomeOwner,
                          Amount = reservation.Amount,
                          ExtraPrice = reservation.ExtraPrice,
                          Discount = reservation.Discount,
                          Total = reservation.Total,
                          PriceType = reservation.PriceType,
                          RoomId = reservation.RoomId,
                          VillaId = reservation.VillaId,

                          ReservationInfos = reservation.ReservationInfos.Select(reservationInfo => new ReservationGetAllResponseDtoReservationInfo()
                          {
                              Name = reservationInfo.Name,
                              Surname = reservationInfo.Surname
                          }).ToList(),
                          ReservationItems = reservation.ReservationItems.Select(reservationItem => new ReservationGetAllResponseDtoReservationItem()
                          {
                              Day = reservationItem.Day,
                              Price = reservationItem.Price
                          }).ToList(),
                          Villa = new()
                          {
                              Name = reservation.Villa.VillaDetails.FirstOrDefault().Name
                          },
                          Room = new()
                          {
                              Name = reservation.Room.RoomDetails.FirstOrDefault().Name
                          }
                      })
                      .ToListAsync();

                PageInfo pageInfo = GeneralFunctions.PageInfoHelper(Page: model.Pagination.Page, Size: model.Pagination.Size, TotalCount: TotalCount);

                return ResponseDto<ICollection<ReservationGetAllResponseDto>>.Success(getAllReservation, 200, pageInfo);
            }
            catch (Exception ex) { return ResponseDto<ICollection<ReservationGetAllResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500); }
        }

        public async Task<ResponseDto<ICollection<ReservationGetAllResponseDto>>> GetAllForVilla(GetAllReservationRequestDto model)
        {
            try
            {
                // Todo: Bu servis yenilendi. Çok detaylı Test yapılacak
                var query = context.Reservations
                       .AsNoTracking()
                       .AsQueryable()
                       .Where(x => x.VillaId == model.VillaId);


                if (!model.HomeOwner && model.AgencyOwner)
                    query = query.Where(x => x.HomeOwner == false);
                else if (model.HomeOwner && !model.AgencyOwner)
                    query = query.Where(x => x.HomeOwner == true); ;


                if (!String.IsNullOrEmpty(model.CustomerSearchName))
                    query = query.Where(x => x.ReservationInfos.Any(x => x.Owner == true && x.Name.Contains(model.CustomerSearchName)));



                if (model.OrderByCustomerName != null)
                {
                    // +OrderByCustomerName
                    if (model.OrderByCustomerName == true)
                        query = query
                            .Select(x => new { Item = x, FirstReservationName = x.ReservationInfos.FirstOrDefault().Name })
                            .OrderBy(x => x.FirstReservationName)
                            .Select(x => x.Item);
                    else
                        query = query
                            .Select(x => new { Item = x, FirstReservationName = x.ReservationInfos.FirstOrDefault().Name })
                            .OrderByDescending(x => x.FirstReservationName)
                            .Select(x => x.Item);
                }
                else if (model.OrderByReservationStatus != null)
                {
                    // +OrderByReservationStatus                        
                    if (model.OrderByReservationStatus == true)
                        query = query.OrderBy(x => x.ReservationStatusType);
                    else
                        query = query.OrderByDescending(x => x.ReservationStatusType);
                }
                else if (model.OrderByCheckIn != null)
                {
                    // +OrderByCheckIn
                    if (model.OrderByCheckIn == true)
                        query = query.OrderBy(x => x.CheckIn);
                    else
                        query = query.OrderByDescending(x => x.CheckIn);
                }
                else if (model.OrderByCheckOut != null)
                {
                    // +OrderByCheckOut
                    if (model.OrderByCheckOut == true)
                        query = query.OrderBy(x => x.CheckOut);
                    else
                        query = query.OrderByDescending(x => x.CheckOut);
                }
                else if (model.OrderByPrice != null)
                {
                    // +OrderByPrice
                    if (model.OrderByPrice == true)
                        query = query.OrderBy(x => x.Total);
                    else
                        query = query.OrderByDescending(x => x.Total);
                }
                else
                {
                    // +CreatedAt
                    query = query.OrderByDescending(x => x.CreatedAt);
                }


                int TotalCount = query.Count();
                query = query.Skip(model.Pagination.Page * model.Pagination.Size).Take(model.Pagination.Size);


                var getAllReservation = await query
                      .Select(reservation => new ReservationGetAllResponseDto()
                      {
                          Id = reservation.Id,
                          ReservationNumber = reservation.ReservationNumber,
                          ReservationChannalType = reservation.ReservationChannalType,
                          ReservationStatusType = reservation.ReservationStatusType,
                          Note = reservation.Note,
                          CheckIn = reservation.CheckIn,
                          CheckOut = reservation.CheckOut,
                          IsDepositPrice = reservation.IsDepositPrice,
                          IsCleaningPrice = reservation.IsCleaningPrice,
                          HomeOwner = reservation.HomeOwner,
                          Amount = reservation.Amount,
                          ExtraPrice = reservation.ExtraPrice,
                          Discount = reservation.Discount,
                          Total = reservation.Total,
                          PriceType = reservation.PriceType,
                          RoomId = reservation.RoomId,
                          VillaId = reservation.VillaId,

                          ReservationInfos = reservation.ReservationInfos.Select(reservationInfo => new ReservationGetAllResponseDtoReservationInfo()
                          {
                              Name = reservationInfo.Name,
                              Surname = reservationInfo.Surname
                          }).ToList(),
                          ReservationItems = reservation.ReservationItems.Select(reservationItem => new ReservationGetAllResponseDtoReservationItem()
                          {
                              Day = reservationItem.Day,
                              Price = reservationItem.Price
                          }).ToList(),
                          Villa = new()
                          {
                              Name = reservation.Villa.VillaDetails.FirstOrDefault().Name
                          },
                          Room = new()
                          {
                              Name = reservation.Room.RoomDetails.FirstOrDefault().Name
                          }
                      })
                      .ToListAsync();

                PageInfo pageInfo = GeneralFunctions.PageInfoHelper(Page: model.Pagination.Page, Size: model.Pagination.Size, TotalCount: TotalCount);

                return ResponseDto<ICollection<ReservationGetAllResponseDto>>.Success(getAllReservation, 200, pageInfo);

                return ResponseDto<ICollection<ReservationGetAllResponseDto>>.Success(getAllReservation, 200, pageInfo);
            }
            catch (Exception ex) { return ResponseDto<ICollection<ReservationGetAllResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500); }
        }

        public async Task<ResponseDto<GetReservationPriceResponseDto>> GetReservationPrice(GetReservationPriceRequestDto model)
        {
            try
            {

                if (model.CheckIn.Date > model.CheckOut.Date)
                    return ResponseDto<GetReservationPriceResponseDto>.Fail(new() { new() { Title = "GetReservationPrice Errors..", Description = "CheckIn Tarihi CheckOut tarihinden büyük olamaz.." } }, 400);
                if (model.CheckIn.Date == model.CheckOut.Date)
                    return ResponseDto<GetReservationPriceResponseDto>.Fail(new() { new() { Title = "GetReservationPrice Errors..", Description = "CheckIn Tarihi CheckOut tarihine eşit olamaz.." } }, 400);
                if (model.VillaId == Guid.Empty && model.RoomId == Guid.Empty)
                    return ResponseDto<GetReservationPriceResponseDto>.Fail(new() { new() { Title = "GetReservationPrice Errors..", Description = "Tesis Id boş olamaz" } }, 400);
                //-----

                // Todo: Ocak ayında fiyat var ama Şubat ayında fiyat yok. CheckIn=30 ocak => CheckOut => 4 Şubat için ocak fiyatı geliyor ama şubat fiyatı olamığı için gelmiyor. Bu durum kontrol edilecek..
                var prices = await priceDateService.GetPriceForDate(new() { VillaId = model.VillaId, RoomId = model.RoomId, CheckIn = model.CheckIn, CheckOut = model.CheckOut });

                if (prices.StatusCode != 200 || prices.Data?.Count == 0)
                    return ResponseDto<GetReservationPriceResponseDto>.Fail(new() { new() { Title = "GetReservationPrice Errors..", Description = "Tesise Ait Fiyat Bulunamadı.." } }, 400);
                //-----

                PriceType priceType = PriceType.TL; // Todo: bu kontrol edilecek
                decimal amount = 0, extra = 0;
                List<GetReservationPriceResponseDtoReservationPriceDay> days = new() { };

                foreach (var item in prices.Data)
                {
                    priceType = item.PriceType;
                    days.Add(new() { Day = item.Date.ToShortDateString(), Price = item.Price });
                    amount = amount + item.Price;
                }

                GetReservationPriceResponseDto response = new()
                {
                    Amount = amount,
                    ExtraPrice = extra,
                    CheckIn = model.CheckIn,
                    CheckOut = model.CheckOut,
                    Total = amount + extra,
                    Days = days,
                    PriceType = priceType
                };

                return ResponseDto<GetReservationPriceResponseDto>.Success(response, 200);
            }
            catch (Exception ex) { return ResponseDto<GetReservationPriceResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500); }
        }

        public async Task<bool> IsAvailible(ReservationIsAvailibleRequestDto model)
        {
            try
            {
                bool result = false;
                if (model.CheckIn.Date < DateTime.Now.Date)
                    return result = false;
                if (model.CheckIn.Date > model.CheckOut.Date)
                    return result = false;
                if (model.CheckIn.Date == model.CheckOut.Date)
                    return result = false;
                if (model.VillaId == null && model.RoomId == null)
                    return result = false;
                //if ((model.CheckOut.Date - model.CheckIn.Date).Days < 5) return result = false;
                //------

                if (model.VillaId != null)
                {
                    if (model.ReservationId != null)
                        result = await context.Reservations
                               .AnyAsync(r => r.Id != model.ReservationId && r.VillaId == model.VillaId && r.ReservationStatusType != ReservationStatusType.Cancaled && r.GeneralStatusType == GeneralStatusType.Active && model.CheckIn < r.CheckOut && model.CheckOut > r.CheckIn);
                    else
                        result = await context.Reservations
                                .AnyAsync(r => r.VillaId == model.VillaId && r.ReservationStatusType != ReservationStatusType.Cancaled && r.GeneralStatusType == GeneralStatusType.Active && model.CheckIn < r.CheckOut && model.CheckOut > r.CheckIn);
                }
                else if (model.RoomId != null)
                {
                    if (model.ReservationId != null)
                        result = await context.Reservations
                               .AnyAsync(r => r.Id != model.ReservationId && r.RoomId == model.RoomId && r.ReservationStatusType != ReservationStatusType.Cancaled && r.GeneralStatusType == GeneralStatusType.Active && model.CheckIn < r.CheckOut && model.CheckOut > r.CheckIn);
                    else
                        result = await context.Reservations
                                .AnyAsync(r => r.RoomId == model.RoomId && r.ReservationStatusType != ReservationStatusType.Cancaled && r.GeneralStatusType == GeneralStatusType.Active && model.CheckIn < r.CheckOut && model.CheckOut > r.CheckIn);
                }
                return !result;
            }
            catch { return false; }
        }

        public async Task<ResponseDto<BaseResponseDto>> ReservationStatusUpdate(ReservationStatusUpdateRequestDto model, Guid userId)
        {
            try
            {
                var getReservation = await context.Reservations.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (getReservation != null)
                {
                    if ((getReservation.ReservationStatusType != model.ReservationStatusType) && model.ReservationStatusType > 0) getReservation.ReservationStatusType = model.ReservationStatusType;

                    context.Reservations.Update(getReservation);
                    var result = await context.SaveChangesAsync();

                    BaseResponseDto baseResponse = new() { Id = getReservation.Id };

                    return ResponseDto<BaseResponseDto>.Success(baseResponse, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Reservation Not Found", Description = "Rezervasyon Bulunamadı.." } }, 400);
            }
            catch (Exception ex) { return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500); }
        }

        public async Task<ResponseDto<BaseResponseDto>> Update(ReservationUpdateRequestDto model, Guid userId)
        {
            try
            {
                Reservation? reservation = await context.Reservations
                       .FirstOrDefaultAsync(x => x.Id == model.Id);

                if (reservation == null)
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Reservation Not Found", Description = "Rezervasyon Bulunamadı.." } }, 400);

                if (!(await IsAvailible(new() { CheckIn = model.CheckIn, CheckOut = model.CheckOut, VillaId = reservation.VillaId, RoomId = reservation.RoomId, ReservationId = reservation.Id })))
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Is Available", Description = "Tesis Belirtilen Tarihler İçin Müsait Değil." } }, 400);
                //-----

                if (reservation.HomeOwner)
                {
                    reservation.CheckIn = model.CheckIn;
                    reservation.CheckOut = model.CheckOut;
                    reservation.UpdatedAt = DateTime.Now;
                    reservation.UpdatedById = userId;
                    context.Reservations.Update(reservation);
                    await context.SaveChangesAsync();
                }
                else
                {
                    var prices = await GetReservationPrice(new() { VillaId = reservation.VillaId, RoomId = reservation.RoomId, CheckIn = model.CheckIn, CheckOut = model.CheckOut });
                    if (prices.StatusCode != 200) return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "GetReservationPrice Error", Description = prices?.Errors?.FirstOrDefault()?.Description } }, 400);
                    //-----

                    decimal total = prices.Data.Total;
                    if (model.Discount > 0) total = total - model.Discount;

                    reservation.CheckIn = model.CheckIn;
                    reservation.CheckOut = model.CheckOut;
                    reservation.Note = model.Note;
                    reservation.Amount = prices.Data.Amount;
                    reservation.PriceType = prices.Data.PriceType;
                    reservation.ExtraPrice = prices.Data.ExtraPrice;
                    reservation.Discount = model.Discount;
                    reservation.Total = total;
                    reservation.UpdatedAt = DateTime.Now;
                    reservation.UpdatedById = userId;

                    context.ReservationItems.RemoveRange(reservation.ReservationItems);
                    context.Reservations.Update(reservation);

                    List<ReservationItem> reservationItems = new();
                    foreach (var day in prices.Data.Days)
                    {
                        reservationItems.Add(new() { Day = Convert.ToDateTime(day.Day), Price = day.Price, GeneralStatusType = GeneralStatusType.Active, ReservationId = reservation.Id, CreatedAt = DateTime.UtcNow });
                    }
                    await context.ReservationItems.AddRangeAsync(reservationItems);
                    //------

                    await context.SaveChangesAsync();
                    //------
                }
                return ResponseDto<BaseResponseDto>.Success(new() { Id = reservation.Id }, 200);
            }

            catch (Exception ex) { return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500); }
        }
    }
}
