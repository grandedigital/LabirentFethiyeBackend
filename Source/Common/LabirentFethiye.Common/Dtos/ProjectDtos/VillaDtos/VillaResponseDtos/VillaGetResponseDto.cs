using LabirentFethiye.Common.Dtos.TownDtos.TownResponseDtos;
using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.VillaDtos.VillaResponseDtos
{
    public class VillaGetResponseDto
    {
        public Guid Id { get; set; }

        public UInt16 Room { get; set; }
        public UInt16 Person { get; set; }
        public UInt16 Bath { get; set; }
        public string? GoogleMap { get; set; }

        public bool OnlineReservation { get; set; }

        public string WaterMaterNumber { get; set; }
        public string ElectricityMeterNumber { get; set; }
        public string InternetMeterNumber { get; set; }
        public string WifiPassword { get; set; }

        public string VillaOwnerName { get; set; }
        public string VillaOwnerPhone { get; set; }
        public string VillaNumber { get; set; }

        public PriceType PriceType { get; set; }

        public bool IsRent { get; set; }
        public bool IsSale { get; set; }

        public int Line { get; set; }

        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }

        public List<VillaGetResponseDtoVillaDetail> VillaDetails { get; set; }
        public List<VillaGetResponseDtoCategories> Categories { get; set; }
        public List<VillaGetResponseDtoPhoto> Photos { get; set; }
        //public virtual List<PriceTable>? PriceTables { get; set; }
        //public virtual List<DistanceRuler>? DistanceRulers { get; set; }

        public List<VillaGetResponseDtoPayments> Payments { get; set; }
        public Guid TownId { get; set; }
        public TownGetResponseDto Town { get; set; }

        public Guid? PersonalId { get; set; }
        public VillaGetResponseDtoPersonal Personal { get; set; }

        public GeneralStatusType GeneralStatusType { get; set; }
    }

    public class VillaGetResponseDtoPersonal
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Phone { get; set; }
    }
    public class VillaGetResponseDtoPayments
    {
        public decimal Amount { get; set; }
        public bool InOrOut { get; set; }
    }
    public class VillaGetResponseDtoPhoto
    {
        public string Title { get; set; }
        public string ImgAlt { get; set; }
        public string ImgTitle { get; set; }
        public string Image { get; set; }
        public string VideoLink { get; set; }
        public int Line { get; set; }
    }
    public class VillaGetResponseDtoCategories
    {
        public Guid Id { get; set; }
        public List<VillaGetResponseDtoCategoriesDetail>? CategoryDetails { get; set; }
    }
    public class VillaGetResponseDtoCategoriesDetail
    {
        public string Name { get; set; }
    }
    public class VillaGetResponseDtoVillaDetail
    {
        public Guid Id { get; set; }
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public string DescriptionShort { get; set; }
        public string DescriptionLong { get; set; }
        public string FeatureTextBlue { get; set; }
        public string FeatureTextRed { get; set; }
        public string FeatureTextWhite { get; set; }
        public GeneralStatusType GeneralStatusType { get; set; }

    }
}
