namespace LabirentFethiye.Domain.Entities.ProjectEntities
{
    public class VillaCategory
    {
        public Guid VillaId { get; set; }
        public Villa Villa { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
