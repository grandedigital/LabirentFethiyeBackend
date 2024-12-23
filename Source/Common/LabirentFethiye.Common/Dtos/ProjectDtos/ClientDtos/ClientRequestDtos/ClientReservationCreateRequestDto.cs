using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientRequestDtos
{
    public class ClientReservationCreateRequestDto
    {
        public string? Note { get; set; }

        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public bool IsDepositPrice { get; set; }
        public bool IsCleaningPrice { get; set; }
        
        public PriceType PriceType { get; set; }

        //public Guid? VillaId { get; set; }
        //public Guid? RoomId { get; set; }
        public string Slug { get; set; }

        public string? IdNo { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
