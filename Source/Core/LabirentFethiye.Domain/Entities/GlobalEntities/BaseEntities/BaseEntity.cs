using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public Guid CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UpdatedById { get; set; }
        public DateTime UpdatedAt { get; set; }
        public GeneralStatusType GeneralStatusType { get; set; }
    }
}
