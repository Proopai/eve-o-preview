using MediatR;

namespace EveOPreview.Mediator.Messages
{
    // Request the live re-application of all hotkeys from the current configuration.
    // Send this after changing a client hotkey from the GUI (issue #13) so the change
    // takes effect without restarting the app.
    sealed class RefreshHotkeys : IRequest
    {
    }
}