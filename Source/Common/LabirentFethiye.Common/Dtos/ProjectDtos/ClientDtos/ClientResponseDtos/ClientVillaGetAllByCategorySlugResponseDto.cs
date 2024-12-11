using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientResponseDtos
{
    public class ClientVillaGetAllByCategorySlugResponseDto
    {
        public string Name { get; set; }
        public string FeatureTextWhite { get; set; }
        public string Slug { get; set; }
        public UInt16 Room { get; set; }
        public UInt16 Person { get; set; }
        public UInt16 Bath { get; set; }
        public string VillaNumber { get; set; }
        public PriceType PriceType { get; set; }
        public bool OnlineReservation { get; set; }
        public string CategoryMetaTitle { get; set; }
        public string CategoryMetaDescription { get; set; }

        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }

        public string Town{ get; set; }
        public string District{ get; set; }
        

        public ICollection<ClientVillaGetAllByCategorySlugResponseDtoPhotos> Photos { get; set; }
    }
    

    public class ClientVillaGetAllByCategorySlugResponseDtoPhotos
    {
        public string Image { get; set; }
    }



    //public class ClientVillaGetAllResponseDto
    //{
    //    public Guid Id { get; set; }
    //    public UInt16 Room { get; set; }
    //    public UInt16 Person { get; set; }
    //    public UInt16 Bath { get; set; }
    //    public string Price { get; set; }
    //    public string VillaNumber { get; set; }
    //    public PriceType PriceType { get; set; }
    //    public bool OnlineReservation { get; set; }
    //    public string Slug { get; set; }

    //    public string CategoryMetaTitle { get; set; }
    //    public string CategoryMetaDescription { get; set; }

    //    public List<ClientVillaGetAllResponseDtoVillaDetail> VillaDetails { get; set; }
    //    public List<ClientVillaGetAllResponseDtoPhotos> Photos { get; set; }
    //    public List<ClientVillaGetAllResponseDtoPriceTable> PriceTables { get; set; }
    //    public TownGetResponseDto Town { get; set; }
    //}
    //public class ClientVillaGetAllResponseDtoVillaDetail
    //{
    //    public string Name { get; set; }
    //    public string DescriptionLong { get; set; }
    //    public string FeatureTextBlue { get; set; }
    //    public string FeatureTextRed { get; set; }
    //    public string FeatureTextWhite { get; set; }
    //}

    //public class ClientVillaGetAllResponseDtoPhotos
    //{
    //    public string Title { get; set; }
    //    public string ImgAlt { get; set; }
    //    public string ImgTitle { get; set; }
    //    public string Image { get; set; }
    //}
    //public class ClientVillaGetAllResponseDtoPriceTable
    //{
    //    public decimal Price { get; set; }
    //    public PriceType PriceType { get; set; }
    //}
}
