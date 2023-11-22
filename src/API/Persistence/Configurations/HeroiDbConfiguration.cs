using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Persistence.Configurations
{
    public class HeroiDbConfiguration : IEntityTypeConfiguration<Heroi>
    {
        public void Configure(EntityTypeBuilder<Heroi> builder)
        {
            builder.ToTable("Herois");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)

                   .ValueGeneratedOnAdd()
                   .IsRequired();

            builder.Property(u => u.Nome)
                  .IsRequired()
                  .HasColumnName("Nome")
                  .HasColumnType("varchar(120)");

            builder.Property(u => u.NomeHeroi)
                   .IsRequired()
                   .HasColumnName("NomeHeroi")
                   .HasColumnType("varchar(120)");

            builder.Property(u => u.DataNascimento)
                   .HasColumnName("DataNascimento")
                   .HasColumnType("datetime(7)");

            builder.Property(u => u.Altura)
                   .IsRequired()
                   .HasColumnName("Altura")
                   .HasColumnType("float");

            builder.Property(u => u.Peso)
                   .IsRequired()
                   .HasColumnName("Peso")
                   .HasColumnType("float");

        }
    }
}
