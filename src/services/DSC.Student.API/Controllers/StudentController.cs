using DSC.Core.Controllers;
using DSC.Core.Mediator;
using DSC.Core.Utils;
using DSC.Student.API.Application.Messages.Commands.StudentCommand;
using DSC.Student.API.Application.Messages.Queries.StudentQuery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DSC.Student.API.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/{version:apiVersion}/student")]
    [ApiController]
    public class StudentController : BaseController
    {

        private readonly IMediatorHandler _mediator;

        public StudentController(IMediatorHandler mediator)
        {
            _mediator = mediator;
        }

        // GET: api/1.0/aluno
        /// <summary>
        /// Obtêm os alunos
        /// </summary>
        /// <returns>Coleção de objetos da classe Aluno</returns>                
        /// <response code="200">Lista dos alunos</response>        
        /// <response code="400">Falha na requisição</response>         
        /// <response code="404">Nenhum aluno foi localizado</response>         
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var getAllStudentQuery = new GetAllStudentQuery();

            var students = await _mediator.SendQuery(getAllStudentQuery);

            return ListUtils.isEmpty(students) ? NotFound() : CustomResponse(students);
        }

        // GET: api/1.0/aluno/5
        /// <summary>
        /// Obtêm as informações do aluno pelo seu Id
        /// </summary>
        /// <param name="id">Código do aluno</param>
        /// <returns>Objetos da classe Aluno</returns>                
        /// <response code="200">Informações do Aluno</response>
        /// <response code="400">Falha na requisição</response>         
        /// <response code="404">O aluno não foi localizado</response>         
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var getByIdStudentQuery = new GetByIdStudentQuery(id);

            var student = await _mediator.SendQuery(getByIdStudentQuery);

            return student == null ? NotFound() : CustomResponse(student);
        }

        // POST api/1.0/aluno
        /// <summary>
        /// Grava o aluno
        /// </summary>   
        /// <remarks>
        /// Exemplo request:
        ///
        ///     POST /Aluno
        ///     {
        ///         "name": "Théo da Silva",
        ///         "dateBirth": "2000-05-27T11:30:07.051Z",
        ///         "rg": "187923607",
        ///         "cpf": "35540251008",      
        ///         "note": "Possui alergia a lactose",
        ///         "courseId": "BCE4F473-3DFA-4FB9-8E1E-5997951F5485",	    
        ///         "guardians": [
        ///             {
        ///                 "name": "João da Silva",
        ///                 "dateBirth": "2021-05-27T11:30:07.051Z",
        ///                 "rg": "187923607",
        ///                 "cpf": "98283526057",
        ///                 "email": "joao@gmail.com",
        ///                 "phone": "51 99999-9999",
        ///                 "cellPhone": "51 99999-9999",
        ///                 "note": "Ligar para a empresa caso não atender"
        ///             },
        ///             {
        ///                 "name": "Maria Santos da Silva",
        ///                 "dateBirth": "2000-05-27T11:30:07.051Z",
        ///                 "rg": "473075404",
        ///                 "cpf": "31869845056",
        ///                 "email": "maria@gmail.com",
        ///                 "phone": "51 8888-8888",
        ///                 "cellPhone": "51 8888-8888",
        ///                 "note": ""
        ///             }
        ///         ],
        ///         "adress": {
        ///             "street": "Rua Sol Nascente",
        ///             "number": "1111",
        ///             "complement": "string",
        ///             "district": "Vista Alegre",
        ///             "zipCode": "93000-000",
        ///             "city": "Ivoti",
        ///             "state": "RS"
        ///         }
        ///     }
        /// </remarks>        
        /// <returns>Retorna objeto criado da classe Aluno</returns>                
        /// <response code="201">O aluno foi incluído corretamente</response>                
        /// <response code="400">Falha na requisição</response>         
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ActionName("NewStudent")]
        public async Task<IActionResult> PostAsync([FromBody] AddStudentCommand command)
        {
            var result = await _mediator.SendCommand(command);

            return result.ValidationResult.IsValid ? CreatedAtAction("NewStudent", new { id = result.response }, command) : CustomResponse(result.ValidationResult);
        }

        // PUT: api/1.0/aluno/5
        /// <summary>
        /// Altera o aluno
        /// </summary>        
        /// <param name="id">Código do aluno</param>        
        /// <response code="204">O aluno foi alterado corretamente</response>                
        /// <response code="400">Falha na requisição</response>         
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] UpdateStudentCommand command)
        {
            command.StudentId = id;

            var result = await _mediator.SendCommand(command);

            return CustomResponse(result.ValidationResult);
        }

        // DELETE: api/1.0/aluno/5
        /// <summary>
        /// Deleta o aluno pelo seu Id
        /// </summary>        
        /// <param name="id">Código do aluno</param>        
        /// <response code="204">O aluno foi excluído corretamente</response>                
        /// <response code="400">Falha na requisição</response>         
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteStudentCommand(id);

            var result = await _mediator.SendCommand(command);

            return CustomResponse(result.ValidationResult);
        }

        // PUT: api/1.0/aluno/alterar-endereco/5
        /// <summary>
        /// Altera o endereço do aluno
        /// </summary>        
        /// <param name="id">Código do aluno</param>        
        /// <response code="200">O aluno foi alterado corretamente</response>                
        /// <response code="400">Falha na requisição</response>         
        [HttpPut("alterar-endereco/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] UpdateAdressStudentCommand command)
        {
            command.StudentId = id;

            var result = await _mediator.SendCommand(command);

            return CustomResponse(result.ValidationResult);
        }

    }
}
