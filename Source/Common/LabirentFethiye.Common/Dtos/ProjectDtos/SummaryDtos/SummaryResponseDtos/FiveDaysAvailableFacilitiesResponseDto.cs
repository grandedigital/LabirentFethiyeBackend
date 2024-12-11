namespace LabirentFethiye.Common.Dtos.ProjectDtos.SummaryDtos.SummaryResponseDtos
{
    public class FiveDaysAvailableFacilitiesResponseDto
    {
        public Guid? VillaId { get; set; }
        public Guid? RoomId { get; set; }
        public string FacilityName { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public string Night { get; set; }
        public decimal Price { get; set; }
    }
}
