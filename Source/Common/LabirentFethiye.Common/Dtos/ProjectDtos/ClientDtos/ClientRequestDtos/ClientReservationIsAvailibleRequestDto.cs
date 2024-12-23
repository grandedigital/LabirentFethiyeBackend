namespace LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientRequestDtos
{
    public class ClientReservationIsAvailibleRequestDto
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        //public Guid? VillaId { get; set; }
        //public Guid? RoomId { get; set; }
        public string Slug { get; set; }
    }
}
