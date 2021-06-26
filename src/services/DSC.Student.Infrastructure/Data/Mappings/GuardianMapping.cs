using DSC.Core.DomainObjects;
using DSC.Student.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DSC.Student.Infrastructure.Data.Mapping
{
    public class GuardianMapping : IEntityTypeConfiguration<GuardianModel>
    {
        public void Configure(EntityTypeBuilder<GuardianModel> builder)
        {

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.OwnsOne(r => r.Rg, tf =>
            {
                tf.Property(r => r.Number)
                    .HasMaxLength(Rg.RgMaxLength)
                    .HasColumnName("Rg")
                    .HasColumnType($"varchar({Rg.RgMaxLength})");
            });

            builder.OwnsOne(r => r.Cpf, tf =>
            {
                tf.Property(r => r.Number)
                    .IsRequired()
                    .HasMaxLength(Cpf.CpfMaxLength)
                    .HasColumnName("Cpf")
                    .HasColumnType($"varchar({Cpf.CpfMaxLength})");
            });

            builder.OwnsOne(r => r.Email, tf =>
            {
                tf.Property(r => r.Address)
                    .IsRequired()
                    .HasMaxLength(Email.AddressMaxLength)
                    .HasColumnName("Email")
                    .HasColumnType($"varchar({Email.AddressMaxLength})");
            });

            builder.OwnsOne(r => r.Phone, tf =>
            {
                tf.Property(r => r.Number)
                    .IsRequired()
                    .HasMaxLength(Phone.PhoneMaxLength)
                    .HasColumnName("Phone")
                    .HasColumnType($"varchar({Phone.PhoneMaxLength})");
            });

            builder.OwnsOne(r => r.CellPhone, tf =>
            {
                tf.Property(r => r.Number)
                    .IsRequired()
                    .HasMaxLength(Phone.PhoneMaxLength)
                    .HasColumnName("CellPhone")
                    .HasColumnType($"varchar({Phone.PhoneMaxLength})");
            });

            // 1 : 1 => Guardian : Note
            builder.HasOne(r => r.Note).WithOne();

            builder.ToTable("Guardians");
        }
    }
}
