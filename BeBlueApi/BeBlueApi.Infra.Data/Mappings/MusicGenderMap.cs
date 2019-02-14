using BeBlueApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeBlueApi.Infra.Data.Mappings
{
    public class MusicGenderMap : IEntityTypeConfiguration<MusicGender>
    {
        public void Configure(EntityTypeBuilder<MusicGender> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.Description)
                .HasColumnType("varchar(250)")
                .HasMaxLength(250)
                .IsRequired();
        }
    }
}
