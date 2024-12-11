using LabirentFethiye.Common.Dtos.TownDtos.TownResponseDtos;
using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.VillaDtos.VillaResponseDtos
{
    public class VillaGetAllResponseDto
    {
        public Guid Id { get; set; }
        public UInt16 Room { get; set; }
        public UInt16 Person { get; set; }
        public UInt16 Bath { get; set; }
        public string GoogleMap { get; set; }
        public string VillaNumber { get; set; }
        public DateTime CreatedAt { get; set; }

        public bool OnlineReservation { get; set; }

        public List<VillaGetResponseDtoVillaDetail>? VillaDetails { get; set; }
        //public virtual List<Photo>? Photos { get; set; }
        //public virtual List<PriceTable>? PriceTables { get; set; }
        //public virtual List<DistanceRuler>? DistanceRulers { get; set; }

        public Guid? TownId { get; set; }
        public TownGetResponseDto Town { get; set; }

        public GeneralStatusType GeneralStatusType { get; set; }

    }
    public class VillaGetAllResponseDtoVillaDetail
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
