using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.RoomAggregate.Events;

using MediatR;

namespace DomeGym.Application.Sessions.Events;

public class SessionScheduledEventHandler : INotificationHandler<SessionScheduledEvent>
{
    private readonly ISessionsRepository _sessionsRepository;

    public SessionScheduledEventHandler(ISessionsRepository sessionsRepository)
    {
        _sessionsRepository = sessionsRepository;
    }

    public async Task Handle(SessionScheduledEvent notification, CancellationToken cancellationToken)
    {
        await _sessionsRepository.AddSessionAsync(notification.Session);
    }
}