namespace LabirentFethiye.Common.Dtos.ProjectDtos.DistanceRulerDtos.DistanceRulerResponseDtos
{
    public class DistanceRulerGetResponseDto
    {
        public Guid Id { get; set; }
        public string Icon { get; set; }
        public string Value { get; set; }

        public List<DistanceRulerGetResponseDtoDistanceRulerDetail>? DistanceRulerDetails { get; set; }

        public Guid? VillaId { get; set; }
        public DistanceRulerGetResponseDtoVilla Villa { get; set; }

        public Guid? HotelId { get; set; }
        public DistanceRulerGetResponseDtoHotel Hotel { get; set; }
    }

    public class DistanceRulerGetResponseDtoDistanceRulerDetail
    {
        public Guid Id { get; set; }
        public string LanguageCode { get; set; }
        public string Name { get; set; }
    }

    public class DistanceRulerGetResponseDtoVilla
    {
        public Guid Id { get; set; }
        public virtual List<DistanceRulerGetResponseDtoVillaDetail> VillaDetails { get; set; }
    }
    public class DistanceRulerGetResponseDtoVillaDetail
    {
        public Guid Id { get; set; }
        public string LanguageCode { get; set; }
        public string Name { get; set; }
    }

    public class DistanceRulerGetResponseDtoHotel
    {
        public Guid Id { get; set; }
        public virtual List<DistanceRulerGetResponseDtoHotelDetail> HotelDetails { get; set; }
    }
    public class DistanceRulerGetResponseDtoHotelDetail
    {
        public Guid Id { get; set; }
        public string LanguageCode { get; set; }
        public string Name { get; set; }
    }
}
