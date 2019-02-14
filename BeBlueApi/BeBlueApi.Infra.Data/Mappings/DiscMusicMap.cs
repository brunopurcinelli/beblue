using BeBlueApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeBlueApi.Infra.Data.Mappings
{
    public class DiscMusicMap : IEntityTypeConfiguration<DiscMusic>
    {
        public void Configure(EntityTypeBuilder<DiscMusic> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(250).IsRequired();
            builder.Property(c => c.IdGender).IsRequired();
            builder.Property(c => c.Price).IsRequired();

            builder.HasOne(c => c.MusicGender).WithMany(m => m.DiscMusics);

        }
    }
}
