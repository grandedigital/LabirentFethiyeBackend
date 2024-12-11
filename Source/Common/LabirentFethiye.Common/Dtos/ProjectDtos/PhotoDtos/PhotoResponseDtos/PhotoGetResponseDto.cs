namespace LabirentFethiye.Common.Dtos.ProjectDtos.PhotoDtos.PhotoResponseDtos
{
    public class PhotoGetResponseDto
    {
        public string Title { get; set; }
        public string ImgAlt { get; set; }
        public string ImgTitle { get; set; }
        public string Image { get; set; }
        public string VideoLink { get; set; }
        public int Line { get; set; }

        public Guid? VillaId { get; set; }
        public PhotoGetResponseDtoVilla? Villa { get; set; }

        public Guid? HotelId { get; set; }
        public PhotoGetResponseDtoHotel? Hotel { get; set; }
    }

    public class PhotoGetResponseDtoVilla
    {
        public List<PhotoGetResponseDtoVillaVillaDetail>? VillaDetails { get; set; }
    }

    public class PhotoGetResponseDtoVillaVillaDetail
    {
        public string LanguageCode { get; set; }
        public string Name { get; set; }
    }

    public class PhotoGetResponseDtoHotel
    {
        public List<PhotoGetResponseDtoHotelHotelDetail>? HotelDetails { get; set; }
    }

    public class PhotoGetResponseDtoHotelHotelDetail
    {
        public string LanguageCode { get; set; }
        public string Name { get; set; }
    }

}
