using LabirentFethiye.Common.Enums;
using LabirentFethiye.Domain.Entities.ProjectEntities;
using Microsoft.AspNetCore.Identity;

namespace LabirentFethiye.Domain.Entities.GlobalEntities.IdentityEntities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Language { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public GeneralStatusType GeneralStatusType { get; set; }

        
        public ICollection<Villa> Villas { get; set; }
    }
}
