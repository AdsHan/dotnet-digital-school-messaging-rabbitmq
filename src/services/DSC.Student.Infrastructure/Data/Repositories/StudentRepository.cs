using DSC.Core.Enums;
using DSC.Student.Domain.Entities;
using DSC.Student.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSC.Student.Infrastructure.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDbContext _dbContext;

        public StudentRepository(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AdressModel> GetAdressByIdAsync(Guid id)
        {
            return await _dbContext.Adresses
                .Where(a => a.Status == EntityStatusEnum.Active)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<GuardianModel> GetGuardianByIdAsync(Guid id)
        {
            return await _dbContext.Guardians
                .Include(a => a.Note)
                .Where(a => a.Status == EntityStatusEnum.Active)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<StudentModel> GetByCpfAsync(string cpf)
        {
            return await _dbContext.Students
                .Where(a => a.Status == EntityStatusEnum.Active)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Cpf.Number == cpf);
        }

        public async Task<StudentModel> GetByIdAsync(Guid id)
        {
            return await _dbContext.Students
                .Where(a => a.Status == EntityStatusEnum.Active)
                .Include(x => x.StudentsGuardians).ThenInclude(i => i.Guardian)
                .Include(a => a.Note)
                .Include(a => a.Adress)
                .Include(a => a.DayNotes)
                .Include(a => a.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<StudentModel> GetByRgAsync(string rg)
        {
            return await _dbContext.Students
                .Where(a => a.Status == EntityStatusEnum.Active)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Rg.Number == rg);
        }

        public async Task<List<StudentModel>> GetAllAsync()
        {
            return await _dbContext.Students
                .Where(a => a.Status == EntityStatusEnum.Active)
                .AsNoTracking()
                .ToListAsync();
        }

        public void Update(StudentModel student)
        {
            // Reforço que as entidades foram alteradas
            _dbContext.Entry(student).State = EntityState.Modified;
            _dbContext.Entry(student.Adress).State = EntityState.Modified;
            _dbContext.Entry(student.Note).State = EntityState.Modified;
            _dbContext.Update(student);
        }

        public void Add(StudentModel student)
        {
            _dbContext.Add(student);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

    }
}
