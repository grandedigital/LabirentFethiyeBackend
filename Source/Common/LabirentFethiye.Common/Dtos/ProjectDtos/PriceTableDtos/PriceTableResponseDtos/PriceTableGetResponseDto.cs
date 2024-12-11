using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.PriceTableDtos.PriceTableResponseDtos
{
    public class PriceTableGetResponseDto
    {
        public Guid Id { get; set; }
        public string Icon { get; set; }
        public decimal Price { get; set; }
        public PriceType PriceType { get; set; }
        public List<PriceTableGetResponseDtoPriceTableDetail>? PriceTableDetails { get; set; }

        public Guid? VillaId { get; set; }
        //public PriceTableGetResponseDtoVilla? Villa { get; set; }

        public Guid? RoomId { get; set; }
        //public PriceTableGetResponseDtoRoom? Room { get; set; }
    }

    public class PriceTableGetResponseDtoPriceTableDetail
    {
        public Guid Id { get; set; }
        public string LanguageCode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    //public class PriceTableGetResponseDtoVilla
    //{
    //    public Guid Id { get; set; }
    //    public List<PriceTableGetResponseDtoVillaDetail>? VillaDetails { get; set; }
    //}
    //public class PriceTableGetResponseDtoVillaDetail
    //{
    //    public Guid Id { get; set; }
    //    public string LanguageCode { get; set; }
    //    public string Name { get; set; }
    //}

    //public class PriceTableGetResponseDtoRoom
    //{
    //    public Guid Id { get; set; }
    //    public List<PriceTableGetResponseDtoRoomDetail>? RoomDetails { get; set; }
    //}
    //public class PriceTableGetResponseDtoRoomDetail
    //{
    //    public Guid Id { get; set; }
    //    public string LanguageCode { get; set; }
    //    public string Name { get; set; }
    //}
}
