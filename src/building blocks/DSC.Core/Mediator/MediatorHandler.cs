using DSC.Core.Commands;
using DSC.Core.Communication;
using MediatR;
using System.Threading.Tasks;

namespace DSC.Core.Mediator
{
    //OBSERVAÇÃO: Existe muita discussão referente ao CQRS retornar valores ou não!
    //            Entendo que no caso deste exemplo não estamos implementando uma arquitetura assíncrona 
    //            baseada em filas, mas sim utilizando "tarefas" assíncronas pode irão fornecer
    //            o resultado de conclusão para o comando async.

    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<BaseResult> SendCommand<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }

        public async Task<object> SendQuery<T>(T query)
        {
            return await _mediator.Send(query);
        }
    }
}