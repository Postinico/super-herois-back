using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Persistence.Configurations
{
    public class SuperpoderDbConfiguration : IEntityTypeConfiguration<Superpoder>
    {
        public void Configure(EntityTypeBuilder<Superpoder> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                   .ValueGeneratedOnAdd()
                   .IsRequired();

            builder.Property(u => u.SuperPoder)
                   .IsRequired()
                   .HasColumnName("SuperPoder")
                   .HasColumnType("varchar(50)");

            builder.Property(u => u.Descricao)
                   .HasColumnName("Descricao")
                   .HasColumnType("varchar(250)");
        }
    }
}
