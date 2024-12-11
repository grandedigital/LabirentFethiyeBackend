using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.PriceDateDtos.PriceDateResponseDtos
{
    public class PriceDateGetAllResponseDto
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }

        public Guid? VillaId { get; set; }
        public PriceDateGetAllResponseDtoVilla? Villa { get; set; }
        public Guid? RoomId { get; set; }
        public PriceDateGetAllResponseDtoRoom? Room { get; set; }
    }

    public class PriceDateGetAllResponseDtoVilla
    {
        public PriceType PriceType { get; set; }
    }

    public class PriceDateGetAllResponseDtoRoom
    {
        public PriceType PriceType { get; set; }

    }
}
