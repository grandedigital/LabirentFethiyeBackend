using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;

namespace LabirentFethiye.Domain.Entities.ProjectEntities
{
    public class Category:WebSiteBaseEntity
    {
        public string Icon { get; set; }
        public int Line { get; set; }
        public ICollection<CategoryDetail> CategoryDetails { get; set; }
        public ICollection<VillaCategory> VillaCategories{ get; set; }
    }
}
