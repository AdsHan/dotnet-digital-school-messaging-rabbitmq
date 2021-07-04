using DSC.Core.Commands;
using DSC.Core.Communication;
using DSC.IntegrationEventLog.Services;
using DSC.MessageBus;
using DSC.MessageBus.Integration;
using DSC.Student.Domain.Entities;
using DSC.Student.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DSC.Student.API.Application.Messages.Commands.StudentCommand
{
    public class StudentCommandHandler : CommandHandler,
        IRequestHandler<AddStudentCommand, BaseResult>,
        IRequestHandler<UpdateStudentCommand, BaseResult>,
        IRequestHandler<DeleteStudentCommand, BaseResult>,
        IRequestHandler<UpdateAdressStudentCommand, BaseResult>,
        IRequestHandler<CheckStudentUsersCreatedCommand, BaseResult>
    {
        private Guid idEvent;

        private readonly IStudentRepository _studentRepository;
        private readonly IMessageBusService _messageBusService;
        private readonly IIntegrationEventLogService _integrationEventLogService;

        public StudentCommandHandler(IStudentRepository studentRepository, IMessageBusService messageBusService, IIntegrationEventLogService integrationEventLogService)
        {
            _studentRepository = studentRepository;
            _messageBusService = messageBusService;
            _integrationEventLogService = integrationEventLogService;
        }

        public async Task<BaseResult> Handle(AddStudentCommand command, CancellationToken cancellationToken)
        {
            if (!command.Validate()) return command.BaseResult;

            var isIncluded = await _studentRepository.GetByCpfAsync(command.Cpf);

            if (isIncluded != null)
            {
                AddError("Este CPF já está em uso por outro aluno!");
                return BaseResult;
            }

            var student = new StudentModel(command.Name, command.DateBirth, command.Rg, command.Cpf, command.Note, command.CourseId);

            var adress = new AdressModel(command.Adress.Street, command.Adress.Number, command.Adress.Complement, command.Adress.District, command.Adress.ZipCode, command.Adress.City, command.Adress.State);

            var guardians = command.Guardians.Select(r => new GuardianModel(r.Name, r.DateBirth, r.Rg, r.Cpf, r.Phone, r.CellPhone, r.Email, r.Note)).ToList();

            var studentsGuardians = new List<StudentGuardianModel>();

            foreach (var item in guardians)
            {
                studentsGuardians.Add(new StudentGuardianModel(student, item));
            }

            student.UpdateAdress(adress);
            student.UpdateGuardians(studentsGuardians);

            _studentRepository.Add(student);

            // Emite o evento para criação dos usuários dos guardiões
            var evt = new CreateUserIntegrationEvent(student.Id);
            foreach (var item in guardians)
            {
                evt.AddGuardian(item.Email.Address, "123456", item.CellPhone.Number);
            }

            try
            {
                idEvent = await _integrationEventLogService.SaveEventAsync(evt);

                await _studentRepository.SaveAsync();

                _messageBusService.Publish(QueueType.NEW_USER, evt);

                await _integrationEventLogService.MarkEventAsPublishedAsync(idEvent);

                BaseResult.response = student.Id;
            }
            catch (Exception ex)
            {
                await _integrationEventLogService.MarkEventAsNoPublishedAsync(idEvent);
                AddError("Erro ao publicar menssagem");
            }

            return BaseResult;
        }

        public async Task<BaseResult> Handle(UpdateStudentCommand command, CancellationToken cancellationToken)
        {
            if (!command.Validate()) return command.BaseResult;

            var student = await _studentRepository.GetByIdAsync(command.StudentId);

            if (student == null)
            {
                AddError("Não foi possível localizar o aluno!");
                return BaseResult;
            }

            // Atualiza o endereço
            var adress = student.Adress;
            adress.Update(command.Adress.Street, command.Adress.Number, command.Adress.Complement, command.Adress.District, command.Adress.ZipCode, command.Adress.City, command.Adress.State);

            // Atualiza os responsáveis
            List<StudentGuardianModel> guardiansToDelete = new List<StudentGuardianModel>();
            List<StudentGuardianModel> guardiansToAdd = new List<StudentGuardianModel>();
            List<StudentGuardianModel> newStudentsGuardians = new List<StudentGuardianModel>();

            foreach (var item in command.Guardians)
            {
                var guardian = await _studentRepository.GetGuardianByIdAsync(item.id);

                if (guardian == null) continue;

                guardian.Update(item.Name, item.DateBirth, item.Rg, item.Cpf, item.Phone, item.CellPhone, item.Email, item.Note);

                newStudentsGuardians.Add(new StudentGuardianModel(student, guardian));
            }

            // Se um responsavel está gravado mas não está na lista de nova (indica que foi retirado)            
            foreach (StudentGuardianModel guardian in student.StudentsGuardians)
            {
                if (newStudentsGuardians.Any(e => e.StudentId == guardian.StudentId && e.GuardianId == guardian.GuardianId) == false)
                {
                    guardiansToDelete.Add(guardian);
                }
            }

            // Se um responsavel está na lista nova mas está na gravada (indica que foi incluso)            
            foreach (StudentGuardianModel Guardian in student.StudentsGuardians)
            {
                StudentGuardianModel isIncluded = student.StudentsGuardians.Where(m => m.GuardianId == Guardian.GuardianId).FirstOrDefault();

                if (isIncluded == null)
                {
                    guardiansToAdd.Add(Guardian);
                }
            }

            student.Update(command.Name, command.DateBirth, command.Rg, command.Cpf, command.Note, command.CourseId);
            student.UpdateAdress(adress);
            student.UpdateGuardians(newStudentsGuardians);

            _studentRepository.Update(student);

            await _studentRepository.SaveAsync();

            return BaseResult;
        }

        public async Task<BaseResult> Handle(DeleteStudentCommand command, CancellationToken cancellationToken)
        {
            if (!command.Validate()) return command.BaseResult;

            var student = await _studentRepository.GetByIdAsync(command.StudentId);

            if (student == null)
            {
                AddError("Não foi possível localizar o aluno!");
                return BaseResult;
            }

            student.Delete();

            _studentRepository.Update(student);

            await _studentRepository.SaveAsync();

            return BaseResult;
        }

        public async Task<BaseResult> Handle(UpdateAdressStudentCommand command, CancellationToken cancellationToken)
        {
            if (!command.Validate()) return command.BaseResult;

            var student = await _studentRepository.GetByIdAsync(command.StudentId);

            if (student == null)
            {
                AddError("Não foi possível localizar o aluno!");
                return BaseResult;
            }

            // Atualiza o endereço
            var adress = student.Adress;
            adress.Update(command.Street, command.Number, command.Complement, command.District, command.ZipCode, command.City, command.State);

            _studentRepository.Update(student);

            await _studentRepository.SaveAsync();

            return BaseResult;
        }
        public async Task<BaseResult> Handle(CheckStudentUsersCreatedCommand command, CancellationToken cancellationToken)
        {
            if (!command.Validate()) return command.BaseResult;

            var student = await _studentRepository.GetByIdAsync(command.StudentId);

            if (student == null)
            {
                AddError("Não foi possível localizar o aluno!");
                return BaseResult;
            }

            student.UpdateAuthCreate(true);

            _studentRepository.Update(student);

            await _studentRepository.SaveAsync();

            return BaseResult;
        }

    }

}