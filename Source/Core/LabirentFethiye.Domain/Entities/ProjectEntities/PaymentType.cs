using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;

namespace LabirentFethiye.Domain.Entities.ProjectEntities
{
    public class PaymentType : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
