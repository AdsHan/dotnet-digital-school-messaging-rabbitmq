using System;

namespace DSC.Student.Domain.Entities
{
    public class StudentGuardianModel
    {
        public StudentGuardianModel()
        {
        }

        public StudentGuardianModel(StudentModel student, GuardianModel guardian)
        {
            Student = student;
            Guardian = guardian;
        }

        public Guid StudentId { get; private set; }
        public Guid GuardianId { get; private set; }

        // EF Relação        
        public StudentModel Student { get; private set; }
        public GuardianModel Guardian { get; private set; }
    }
}
