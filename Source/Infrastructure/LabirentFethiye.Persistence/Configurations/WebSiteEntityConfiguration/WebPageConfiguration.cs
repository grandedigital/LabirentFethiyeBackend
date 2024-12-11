using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LabirentFethiye.Domain.Entities.WebSiteEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;

namespace LabirentFethiye.Persistence.Configurations.WebSiteEntityConfiguration
{
    public class WebPageConfiguration : WebSiteBaseConfiguration<WebPage>
    {
        public override void Configure(EntityTypeBuilder<WebPage> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(WebPage));
        }
    }
}
