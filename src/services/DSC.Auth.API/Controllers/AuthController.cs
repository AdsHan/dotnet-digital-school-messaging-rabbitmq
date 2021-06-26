using DSC.Auth.API.Application.Messages.Commands.TokenCommand;
using DSC.Auth.API.Application.Messages.Commands.UserCommand;
using DSC.Auth.API.Application.Messages.Queries.UserQuery;
using DSC.Core.Controllers;
using DSC.Core.Mediator;
using DSC.Core.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DSC.Auth.API.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/{version:apiVersion}/auth")]
    [ApiController]
    public class AuthController : BaseController
    {

        private readonly IMediatorHandler _mediator;

        public AuthController(IMediatorHandler mediator)
        {
            _mediator = mediator;
        }

        // GET: api/1.0/identidade
        /// <summary>
        /// Obtêm os usuários
        /// </summary>
        /// <returns>Coleção de objetos da classe Usuário</returns>                
        /// <response code="200">Lista dos usuários</response>        
        /// <response code="400">Falha na requisição</response>         
        /// <response code="404">Nenhum usuário foi localizado</response>         
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var getAllUsersQuery = new GetAllUserQuery();

            var users = await _mediator.SendQuery(getAllUsersQuery);

            return ListUtils.isEmpty(users) ? NotFound() : CustomResponse(users);
        }

        // POST api/1.0/identidade
        /// <summary>
        /// Grava o usuário
        /// </summary>   
        /// <remarks>
        /// Exemplo request:
        ///
        ///     POST /Usuario
        ///     {
        ///         "email": "mario@gmail.com",
        ///         "password": "sys123",
        ///         "phone": "99 9999-9999",
        ///     }
        /// </remarks>        
        /// <returns>Retorna objeto criado da classe Usuario</returns>                
        /// <response code="201">O usuário foi incluído corretamente</response>                
        /// <response code="400">Falha na requisição</response>         
        [HttpPost("new-user")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ActionName("NewUser")]
        public async Task<IActionResult> PostAsync([FromBody] AddUserCommand command)
        {
            var result = await _mediator.SendCommand(command);

            return result.ValidationResult.IsValid ? CreatedAtAction("NewUser", new { id = result.response }, command) : CustomResponse(result.ValidationResult);
        }

        // POST api/1.0/identidade
        /// <summary>
        /// Efetua o login do usuário
        /// </summary>   
        /// <remarks>
        /// Exemplo request:
        ///
        ///     POST /Usuario
        ///     {
        ///         "email": "mario@gmail.com",
        ///         "password": "sys123",
        ///     }
        /// </remarks>        
        /// <returns>Token de autenticação</returns>                
        /// <response code="200">Foi realizado o login corretamente</response>                
        /// <response code="400">Falha na requisição</response>         
        [HttpPost("auth-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignInAsync([FromBody] SignInUserCommand command)
        {
            var result = await _mediator.SendCommand(command);

            return result.ValidationResult.IsValid ? CustomResponse(result) : CustomResponse(result.ValidationResult);
        }

        // POST api/1.0/identidade
        /// <summary>
        /// Efetua o atualização do token
        /// </summary>   
        /// <remarks>
        /// Exemplo request:
        ///
        ///     POST /Usuario
        ///     {
        ///         "email": "mario@gmail.com",
        ///         "password": "sys123",
        ///         "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Im1hcmlvQGdtYWlsLmNvbSIsImp0aSI6ImM3MTBjZjlhLTU4YzEtNDAxNy04ZTFlLWE2YjI2ZDUzZTRjOSIsImV4cCI6MTYyMzI2OTUzMSwiaXNzIjoiVGVzdGUiLCJhdWQiOiJUZXN0ZSJ9.15vHaGHq6Fi9wwqNssEVAAbydItTYNVpgPsnrAPPBFU",
        ///         
        ///     }
        /// </remarks>        
        /// <returns>Novo Token de autenticação</returns>                
        /// <response code="200">Foi realizada a atualização correta do token</response>                
        /// <response code="400">Falha na requisição</response>         
        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenCommand command)
        {
            var result = await _mediator.SendCommand(command);

            return result.ValidationResult.IsValid ? CustomResponse(result) : CustomResponse(result.ValidationResult);
        }
    }
}
