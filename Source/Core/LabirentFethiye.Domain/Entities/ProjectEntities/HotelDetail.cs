using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;

namespace LabirentFethiye.Domain.Entities.ProjectEntities
{
    public class HotelDetail:BaseEntity
    {
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public string DescriptionShort { get; set; }
        public string DescriptionLong { get; set; }

        public string FeatureTextBlue { get; set; }
        public string FeatureTextRed { get; set; }
        public string FeatureTextWhite { get; set; }

        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}
