using DSC.Core.DomainObjects;
using DSC.Student.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DSC.Student.Infrastructure.Data.Mapping
{
    public class StudentMapping : IEntityTypeConfiguration<StudentModel>
    {
        public void Configure(EntityTypeBuilder<StudentModel> builder)
        {

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.OwnsOne(a => a.Rg, tf =>
            {
                tf.Property(a => a.Number)
                    .HasMaxLength(Rg.RgMaxLength)
                    .HasColumnName("Rg")
                    .HasColumnType($"varchar({Rg.RgMaxLength})");
            });

            builder.OwnsOne(a => a.Cpf, tf =>
            {
                tf.Property(a => a.Number)
                    .IsRequired()
                    .HasMaxLength(Cpf.CpfMaxLength)
                    .HasColumnName("Cpf")
                    .HasColumnType($"varchar({Cpf.CpfMaxLength})");
            });

            // 1 : 1 => Student : Adress
            builder.HasOne(a => a.Adress).WithOne();

            // 1 : 1 => Student : Note
            builder.HasOne(a => a.Note).WithOne();

            // 1 : N => Student : Day Notes
            builder.HasMany(a => a.DayNotes)
                .WithOne(t => t.Student)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // N : 1 => Student : Course
            builder.HasOne(a => a.Course)
                .WithMany(t => t.Students)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Students");
        }
    }
}
