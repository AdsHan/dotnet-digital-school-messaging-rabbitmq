using DSC.Core.DomainObjects;
using DSC.Student.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DSC.Student.Infrastructure.Data.Mapping
{
    public class SchoolMapping : IEntityTypeConfiguration<SchoolModel>
    {
        public void Configure(EntityTypeBuilder<SchoolModel> builder)
        {

            builder.HasKey(e => e.Id);

            builder.Property(e => e.CorporateName)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(e => e.TradeName)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.OwnsOne(e => e.Cnpj, tf =>
            {
                tf.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(Cnpj.CnpjMaxLength)
                    .HasColumnName("Cpnj")
                    .HasColumnType($"varchar({Cnpj.CnpjMaxLength})");
            });

            builder.OwnsOne(e => e.Email, tf =>
            {
                tf.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(Email.AddressMaxLength)
                    .HasColumnName("Email")
                    .HasColumnType($"varchar({Email.AddressMaxLength})");
            });

            builder.OwnsOne(e => e.Phone, tf =>
            {
                tf.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(Phone.PhoneMaxLength)
                    .HasColumnName("Phone")
                    .HasColumnType($"varchar({Phone.PhoneMaxLength})");
            });

            builder.OwnsOne(e => e.CellPhone, tf =>
            {
                tf.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(Phone.PhoneMaxLength)
                    .HasColumnName("CellPhone")
                    .HasColumnType($"varchar({Phone.PhoneMaxLength})");
            });

            // 1 : 1 => School : Adress
            builder.HasOne(e => e.Adress).WithOne();

            // 1 : 1 => School : Note
            builder.HasOne(e => e.Adress).WithOne();

            // 1 : N => School : Course
            builder.HasMany(e => e.Courses)
                .WithOne(t => t.School)
                .HasForeignKey(t => t.SchoolId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Schools");

        }
    }
}
