using DSC.Student.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DSC.Student.Infrastructure.Data.Mapping
{
    public class StudentGuardianMapping : IEntityTypeConfiguration<StudentGuardianModel>
    {
        public void Configure(EntityTypeBuilder<StudentGuardianModel> builder)
        {

            builder.HasKey(a => new { a.StudentId, a.GuardianId });

            // N : N => Student : Guardian
            builder
                .HasOne(a => a.Student)
                .WithMany(a => a.StudentsGuardians)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(a => a.Guardian)
                .WithMany(a => a.StudentsGuardians)
                .HasForeignKey(a => a.GuardianId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("StudentsGuardian");

        }
    }
}
