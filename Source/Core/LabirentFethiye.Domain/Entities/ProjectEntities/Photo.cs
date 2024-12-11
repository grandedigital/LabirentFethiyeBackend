using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;

namespace LabirentFethiye.Domain.Entities.ProjectEntities
{
    public class Photo : BaseEntity
    {
        public string Title { get; set; }
        public string ImgAlt { get; set; }
        public string ImgTitle { get; set; }
        public string Image { get; set; }
        public string VideoLink { get; set; }
        public int Line { get; set; }

        public Guid? VillaId { get; set; }
        public Villa Villa { get; set; }
        public Guid? HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public Guid? RoomId { get; set; }
        public Room Room { get; set; }
    }
}
