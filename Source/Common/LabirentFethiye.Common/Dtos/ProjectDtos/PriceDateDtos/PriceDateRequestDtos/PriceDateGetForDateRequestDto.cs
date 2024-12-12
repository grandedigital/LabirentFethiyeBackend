using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.PriceDateDtos.PriceDateRequestDtos
{
    public class PriceDateGetForDateRequestDto
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public Guid? VillaId { get; set; }
        public Guid? RoomId { get; set; }
        public Guid? Id { get; set; }
    }
}
