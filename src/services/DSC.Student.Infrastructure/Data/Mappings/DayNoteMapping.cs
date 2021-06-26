using DSC.Student.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DSC.Student.Infrastructure.Data.Mapping
{
    public class DayNoteMapping : IEntityTypeConfiguration<DayNoteModel>
    {
        public void Configure(EntityTypeBuilder<DayNoteModel> builder)
        {

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Text)
                .IsRequired()
                .HasColumnType("varchar(8000)");

            // N : 1 => Day Note : Student
            builder.HasOne(r => r.Student)
                .WithMany(r => r.DayNotes)
                .HasForeignKey(r => r.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("DaysNotes");
        }
    }
}
