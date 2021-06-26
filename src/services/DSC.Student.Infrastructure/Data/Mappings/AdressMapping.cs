using DSC.Student.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DSC.Student.Infrastructure.Data.Mapping
{
    public class AdressConfigurations : IEntityTypeConfiguration<AdressModel>
    {
        public void Configure(EntityTypeBuilder<AdressModel> builder)
        {

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Street)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(e => e.Number)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(e => e.ZipCode)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(e => e.Complement)
                .HasColumnType("varchar(250)");

            builder.Property(e => e.District)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(e => e.City)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(e => e.State)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.ToTable("Adresses");

        }
    }
}
