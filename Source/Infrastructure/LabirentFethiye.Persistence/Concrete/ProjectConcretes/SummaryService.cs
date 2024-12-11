using LabirentFethiye.Application.Abstracts.ProjectInterfaces;
using LabirentFethiye.Common.Dtos.ProjectDtos.SummaryDtos.SummaryResponseDtos;
using LabirentFethiye.Common.Enums;
using LabirentFethiye.Common.Responses;
using LabirentFethiye.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LabirentFethiye.Persistence.Concrete.ProjectConcretes
{
    public class SummaryService: ISummaryService
    {
        private readonly AppDbContext context;

        public SummaryService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<ResponseDto<ICollection<CommentsAwaitingApprovalResponseDto>>> CommentsAwaitingApproval()
        {
            try
            {
                var getAllCategory = await context.Comments
                    .Where(x => x.GeneralStatusType == GeneralStatusType.Passive)
                    .OrderBy(x => x.CreatedAt)
                    .Select(item => new CommentsAwaitingApprovalResponseDto()
                    {
                        Id = item.Id,
                        Name = item.Name
                    })
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<CommentsAwaitingApprovalResponseDto>>.Success(getAllCategory, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<CommentsAwaitingApprovalResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<FiveDaysAvailableFacilitiesResponseDto>>> FiveDaysAvailableFacilities()
        {
            try
            {
                List<FiveDaysAvailableFacilitiesResponseDto> result = new();

                // Todo: Rezervasyon servisi farklı bir projeye bağlanabilir....

                //var reservations = await _context.Reservations
                //    .AsNoTracking()
                //    .Where(x =>
                //        (x.ReservationStatusType != ReservationStatusType.Cancaled) &&
                //        (x.Room.Hotel.CompanyId == CompanyId || x.Villa.CompanyId == CompanyId) &&
                //        (x.CheckOut.Date >= DateTime.Now.Date || x.CheckIn.Date == DateTime.Now.Date) &&
                //        (x.CheckIn.Year >= DateTime.Now.Year && x.CheckOut.Year >= DateTime.Now.Year)
                //    )
                //    .Include(x => x.Villa).ThenInclude(x => x.VillaDetails)
                //    .Include(x => x.Room).ThenInclude(x => x.RoomDetails)
                //    .OrderBy(x => x.CheckIn)
                //    .ToListAsync();

                //if (reservations != null || reservations.Count != 0)
                //{
                //    for (int i = 0; i < reservations.Count; i++)
                //    {
                //        if (i < reservations.Count - 1)
                //        {

                //            if ((reservations[i + 1].CheckIn.Date - reservations[i].CheckOut.Date).Days <= 5 && (reservations[i + 1].CheckIn.Date - reservations[i].CheckOut.Date).Days > 0)
                //            {
                //                result.Add(new()
                //                {
                //                    VillaId = reservations[i].VillaId,
                //                    RoomId = reservations[i].RoomId,
                //                    FacilityName = reservations[i].Villa != null ? reservations[i].Villa.VillaDetails.FirstOrDefault().Name : reservations[i].Room.RoomDetails.FirstOrDefault().Name,
                //                    CheckIn = reservations[i].CheckOut.Date.ToShortDateString(),
                //                    CheckOut = reservations[i + 1].CheckIn.Date.ToShortDateString(),
                //                    Night = (reservations[i + 1].CheckIn.Date - reservations[i].CheckOut.Date).Days.ToString(),
                //                    Price = 0
                //                });

                //            }

                //            //if ((reservations[i + 1].CheckOut.Date - reservations[i].CheckIn.Date).Days <= 5)
                //            //{
                //            //    result.Add(new()
                //            //    {
                //            //        VillaId = reservations[i].VillaId,
                //            //        RoomId = reservations[i].RoomId,
                //            //        FacilityName = reservations[i].Villa != null ? reservations[i].Villa.VillaDetails.FirstOrDefault().Name : reservations[i].Room.RoomDetails.FirstOrDefault().Name,
                //            //        CheckIn = reservations[i].CheckIn.Date.ToShortDateString(),
                //            //        CheckOut = reservations[i + 1].CheckOut.Date.ToShortDateString(),
                //            //        Night = (reservations[i + 1].CheckOut.Date - reservations[i].CheckIn.Date).Days.ToString(),
                //            //        Price = 0
                //            //    });
                //            //}
                //        }
                //    }
                //}

                return ResponseDto<ICollection<FiveDaysAvailableFacilitiesResponseDto>>.Success(result, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<FiveDaysAvailableFacilitiesResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<ReservationsAwaitingConfirmationResponseDto>>> ReservationsAwaitingConfirmation()
        {
            try
            {
                // Todo: Rezervasyon servisi farklı bir projeye bağlanabilir....

                List<ReservationsAwaitingConfirmationResponseDto> getAllCategory = new();

                //getAllCategory = await _context.Reservations
                //    .Where(x => x.ReservationStatusType == ReservationStatusType.Option && (x.Room.Hotel.CompanyId == CompanyId || x.Villa.CompanyId == CompanyId))
                //    .Include(x => x.ReservationInfos.Where(x => x.Owner == true))
                //    .Include(x => x.Villa).ThenInclude(x => x.VillaDetails)
                //    .Include(x => x.Room).ThenInclude(x => x.RoomDetails)
                //    .OrderBy(x => x.CreatedAt)
                //    .Select(item => new ReservationsAwaitingConfirmationResponseDto()
                //    {
                //        Id = item.Id,
                //        CheckIn = item.CheckIn,
                //        CheckOut = item.CheckOut,
                //        CustomerName = item.ReservationInfos.FirstOrDefault().Name + " " + item.ReservationInfos.FirstOrDefault().Surname,
                //        ReservationNumber = item.ReservationNumber,
                //        FacilityName = item.Villa != null ? item.Villa.VillaDetails.FirstOrDefault().Name : item.Room.RoomDetails.FirstOrDefault().Name,
                //    })
                //    .AsNoTracking()
                //    .ToListAsync();

                return ResponseDto<ICollection<ReservationsAwaitingConfirmationResponseDto>>.Success(getAllCategory, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<ReservationsAwaitingConfirmationResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<ThreeDayCheckInCheckOutReservationResponseDto>>> ThreeDayCheckInCheckOutReservation()
        {
            try
            {
                // Todo: Rezervasyon servisi farklı bir projeye bağlanabilir....

                List<ThreeDayCheckInCheckOutReservationResponseDto> getAllCategory = new();

                //DateTime threeDayLater = DateTime.Now.AddDays(3).Date;
                //getAllCategory = await _context.Reservations
                //     .Where(x => x.Room.Hotel.CompanyId == CompanyId || x.Villa.CompanyId == CompanyId)
                //     .Where(x => x.CheckIn.Date <= threeDayLater || x.CheckOut <= threeDayLater)
                //     .Include(x => x.ReservationInfos.Where(x => x.Owner == true))
                //     .Include(x => x.Villa).ThenInclude(x => x.VillaDetails)
                //     .Include(x => x.Room).ThenInclude(x => x.RoomDetails)
                //     .OrderBy(x => x.CreatedAt)
                //     .Select(item => new ThreeDayCheckInCheckOutReservationResponseDto()
                //     {
                //         Id = item.Id,
                //         CheckIn = item.CheckIn,
                //         CheckOut = item.CheckOut,
                //         CustomerName = item.ReservationInfos.FirstOrDefault().Name + " " + item.ReservationInfos.FirstOrDefault().Surname,
                //         ReservationNumber = item.ReservationNumber,
                //         FacilityName = item.Villa != null ? item.Villa.VillaDetails.FirstOrDefault().Name : item.Room.RoomDetails.FirstOrDefault().Name,
                //     })
                //     .AsNoTracking()
                //     .ToListAsync();

                return ResponseDto<ICollection<ThreeDayCheckInCheckOutReservationResponseDto>>.Success(getAllCategory, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<ThreeDayCheckInCheckOutReservationResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }
    }
}
