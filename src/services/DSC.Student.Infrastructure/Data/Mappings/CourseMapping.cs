using DSC.Student.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DSC.Student.Infrastructure.Data.Mapping
{
    public class CourseMapping : IEntityTypeConfiguration<CourseModel>
    {
        public void Configure(EntityTypeBuilder<CourseModel> builder)
        {

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            // 1 : 1 => Course : Note
            builder.HasOne(t => t.Note).WithOne();

            // N : 1 => Course : School
            builder.HasOne(t => t.School)
                .WithMany(e => e.Courses)
                .OnDelete(DeleteBehavior.Restrict);

            // 1 : N  => Course : Student
            builder.HasMany(t => t.Students)
                .WithOne(a => a.Course)
                .HasForeignKey(a => a.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Courses");

        }
    }
}
