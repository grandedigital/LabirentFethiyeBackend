namespace LabirentFethiye.Common.Dtos.ProjectDtos.DistanceRulerDtos.DistanceRulerResponseDtos
{
    public class DistanceRulerGetAllResponseDto
    {
        public Guid Id { get; set; }
        public string Icon { get; set; }
        public string Value { get; set; }

        public virtual List<DistanceRulerGetAllResponseDtoDistanceRulerDetail>? DistanceRulerDetails { get; set; }

        public Guid? VillaId { get; set; }
        public virtual DistanceRulerGetAllResponseDtoVilla Villa { get; set; }

        public Guid? HotelId { get; set; }
        public virtual DistanceRulerGetAllResponseDtoHotel Hotel { get; set; }
    }

    public class DistanceRulerGetAllResponseDtoDistanceRulerDetail
    {
        public Guid Id { get; set; }
        public string LanguageCode { get; set; }
        public string Name { get; set; }
    }

    public class DistanceRulerGetAllResponseDtoVilla
    {
        public Guid Id { get; set; }
        public virtual List<DistanceRulerGetAllResponseDtoVillaDetail> VillaDetails { get; set; }
    }
    public class DistanceRulerGetAllResponseDtoVillaDetail
    {
        public Guid Id { get; set; }
        public string LanguageCode { get; set; }
        public string Name { get; set; }
    }

    public class DistanceRulerGetAllResponseDtoHotel
    {
        public Guid Id { get; set; }
        public virtual List<DistanceRulerGetAllResponseDtoHotelDetail> HotelDetails { get; set; }
    }
    public class DistanceRulerGetAllResponseDtoHotelDetail
    {
        public Guid Id { get; set; }
        public string LanguageCode { get; set; }
        public string Name { get; set; }
    }
}
