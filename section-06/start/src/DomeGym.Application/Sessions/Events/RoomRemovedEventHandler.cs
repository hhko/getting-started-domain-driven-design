using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.GymAggregate.Events;

using MediatR;

namespace DomeGym.Application.Sessions.Events.RoomRemoved;

public class RoomRemovedEventHandler : INotificationHandler<RoomRemovedEvent>
{
    private readonly ISessionsRepository _sessionsRepository;

    public RoomRemovedEventHandler(ISessionsRepository sessionsRepository)
    {
        _sessionsRepository = sessionsRepository;
    }

    public async Task Handle(RoomRemovedEvent notification, CancellationToken cancellationToken)
    {
        var sessions = await _sessionsRepository.ListByRoomIdAsync(notification.Room.Id);

        sessions.ForEach(session => session.Cancel());

        await _sessionsRepository.RemoveRangeAsync(sessions);
    }
}