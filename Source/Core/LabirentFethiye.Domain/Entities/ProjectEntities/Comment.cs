using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;

namespace LabirentFethiye.Domain.Entities.ProjectEntities
{
    public class Comment:BaseEntity
    {
        public string Title { get; set; }
        public string CommentText { get; set; }
        public decimal Rating { get; set; }
        public string Video { get; set; }

        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public Guid? VillaId { get; set; }
        public Villa Villa { get; set; }

        public Guid? HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}
