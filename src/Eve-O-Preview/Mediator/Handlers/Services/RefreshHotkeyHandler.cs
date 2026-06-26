using System.Threading;
using System.Threading.Tasks;
using EveOPreview.Mediator.Messages;
using EveOPreview.Services;
using MediatR;

namespace EveOPreview.Mediator.Handlers.Services
{
    sealed class RefreshHotkeysHandler : IRequestHandler<RefreshHotkeys>
    {
        private readonly IThumbnailManager _manager;

        public RefreshHotkeysHandler(IThumbnailManager manager)
        {
            this._manager = manager;
        }

        public Task<Unit> Handle(RefreshHotkeys message, CancellationToken cancellationToken)
        {
            this._manager.RefreshHotkeys();

            return Unit.Task;
        }
    }
}