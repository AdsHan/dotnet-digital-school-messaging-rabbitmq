using DSC.Core.Commands;
using DSC.Core.Communication;
using System.Threading.Tasks;

namespace DSC.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task<BaseResult> SendCommand<T>(T command) where T : Command;
        Task<object> SendQuery<T>(T query);
    }
}