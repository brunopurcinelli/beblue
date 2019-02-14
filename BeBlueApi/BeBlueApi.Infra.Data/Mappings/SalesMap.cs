using BeBlueApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeBlueApi.Infra.Data.Mappings
{
    public class SalesMap : IEntityTypeConfiguration<Sales>
    {
        public void Configure(EntityTypeBuilder<Sales> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.SalesDate)
                .IsRequired();

            builder.Property(c => c.TotalAmount)
                .HasColumnType("numeric(18,2)")
                .IsRequired();

            builder.Property(c => c.TotalCashback)
                .HasColumnType("numeric(18,2)")
                .IsRequired();

            //builder.Property(c => c.Lines)
            //    .HasColumnType("numeric(18,2)")
            //    .IsRequired();
        }
    }
}
