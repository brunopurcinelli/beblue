using BeBlueApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeBlueApi.Infra.Data.Mappings
{
    public class MusicGenderMap : IEntityTypeConfiguration<MusicGender>
    {
        public void Configure(EntityTypeBuilder<MusicGender> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Description).HasMaxLength(250);
        }
    }
}
