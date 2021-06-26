using DSC.Student.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DSC.Student.Infrastructure.Data
{
    public class StudentDbContext : DbContext
    {

        public StudentDbContext()
        {

        }

        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {

        }

        public DbSet<SchoolModel> Schools { get; set; }
        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<StudentModel> Students { get; set; }
        public DbSet<GuardianModel> Guardians { get; set; }
        public DbSet<AdressModel> Adresses { get; set; }
        public DbSet<NoteModel> Notes { get; set; }
        public DbSet<StudentGuardianModel> StudentsGuardian { get; set; }
        public DbSet<DayNoteModel> DaysNotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
