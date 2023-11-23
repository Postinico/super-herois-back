using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Persistence.Configurations
{
    public class HeroiSuperpoderDbConfiguration : IEntityTypeConfiguration<HeroiSuperpoder>
    {
        public void Configure(EntityTypeBuilder<HeroiSuperpoder> builder)
        {
            builder.ToTable("HeroisSuperpoderes");

            builder.HasKey(s => new { s.HeroiId, s.SuperpoderId });

            builder.Property(u => u.HeroiId)
                   .IsRequired()
                   .HasColumnName("HeroiId");

            builder.Property(u => u.SuperpoderId)
                   .IsRequired()
                   .HasColumnName("SuperpoderId");
        }
    }
}
