using LabirentFethiye.Application.Abstracts.GlobalInterfaces;
using LabirentFethiye.Infastructure.Abstract.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LabirentFethiye.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        
        private readonly IMailService _mailService;

        public HomeController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var result = await _mailService.ReservationCreateSendMailAsync(new Common.Dtos.GlobalDtos.MailDtos.MailRequestDtos.ReservationCreateMailRequestDto()
            //{

            //    CheckIn = new DateTime(2024, 01, 01),
            //    CheckOut = new DateTime(2024, 01, 10),

            //    Note = "test 1",
            //    PriceType = Common.Enums.PriceType.GBP,
            //    ReservationChannalType = Common.Enums.ReservationChannalType.WebSite,
            //    ReservationNumber = "648645614",
            //    ReservationStatusType = Common.Enums.ReservationStatusType.Option,
            //    Amount = 10000,
            //    Discount = 1000,
            //    ExtraPrice = 150,
            //    Total = 11150,
            //    Villa = new()
            //    {
            //        Name = "Villa 1",
            //        Person = 4
            //    },
            //    ReservationInfo = new()
            //    {
            //        Name = "barış",
            //        Surname = "Güneş",
            //        Phone = "05382403883",
            //        IdNo = "9641684184"
            //    }

            //});

            return StatusCode(200, "Labirent Fethiye");
        }
    }
}
