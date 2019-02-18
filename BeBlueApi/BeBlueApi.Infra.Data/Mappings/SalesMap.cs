using BeBlueApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeBlueApi.Infra.Data.Mappings
{
    public class SalesMap : IEntityTypeConfiguration<Sales>
    {
        public void Configure(EntityTypeBuilder<Sales> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.SalesDate);
            builder.Property(c => c.TotalAmount);
            builder.Property(c => c.TotalCashback);
        }
    }
}
