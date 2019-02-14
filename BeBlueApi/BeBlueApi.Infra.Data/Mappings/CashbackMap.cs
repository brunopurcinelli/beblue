using BeBlueApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeBlueApi.Infra.Data.Mappings
{
    public class CashbackMap : IEntityTypeConfiguration<Cashback>
    {
        public void Configure(EntityTypeBuilder<Cashback> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.IdGender).IsRequired();
            builder.Property(c => c.WeekDay).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Percent).IsRequired();

            builder.HasOne(c => c.MusicGender).WithMany(m => m.Cashbacks);

        }
    }
}
