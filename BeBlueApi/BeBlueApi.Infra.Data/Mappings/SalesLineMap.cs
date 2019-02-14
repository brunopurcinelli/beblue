using BeBlueApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeBlueApi.Infra.Data.Mappings
{
    public class SalesLineMap : IEntityTypeConfiguration<SalesLine>
    {
        public void Configure(EntityTypeBuilder<SalesLine> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.IdSales).IsRequired();
            builder.Property(c => c.IdItem).IsRequired();
            builder.Property(c => c.DiscName).HasMaxLength(250);
            builder.Property(c => c.Quantity).IsRequired();
            builder.Property(c => c.PriceUnit).IsRequired();
            builder.Property(c => c.SalesPrice).IsRequired();

            builder.HasOne(c => c.Sales).WithMany(m => m.Lines);
            builder.HasOne(c => c.DiscMusic).WithMany(m => m.SalesLines);
        }
    }
}
