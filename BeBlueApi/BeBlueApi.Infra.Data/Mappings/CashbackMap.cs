using BeBlueApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeBlueApi.Infra.Data.Mappings
{
    public class CashbackMap : IEntityTypeConfiguration<Cashback>
    {
        public void Configure(EntityTypeBuilder<Cashback> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.MusicGender)
                .HasColumnType("varchar(250)")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(c => c.WeekDay)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Percent)
                .HasColumnType("numeric(18,2)")
                .IsRequired();
        }
    }
}
