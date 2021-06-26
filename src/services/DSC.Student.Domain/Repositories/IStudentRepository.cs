using DSC.Core.Data;
using DSC.Student.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace DSC.Student.Domain.Repositories
{
    public interface IStudentRepository : IRepository<StudentModel>
    {
        Task<StudentModel> GetByCpfAsync(string cpf);
        Task<StudentModel> GetByRgAsync(string rg);
        Task<AdressModel> GetAdressByIdAsync(Guid id);
        Task<GuardianModel> GetGuardianByIdAsync(Guid id);
    }
}
