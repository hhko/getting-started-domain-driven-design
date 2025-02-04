using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.GymAggregate.Events;

using MediatR;

namespace DomeGym.Application.Rooms.Events.RoomRemoved;

public class RoomRemovedEventHandler : INotificationHandler<RoomRemovedEvent>
{
    private readonly IRoomsRepository _roomsRepository;

    public RoomRemovedEventHandler(IRoomsRepository roomsRepository)
    {
        _roomsRepository = roomsRepository;
    }

    public async Task Handle(RoomRemovedEvent notification, CancellationToken cancellationToken)
    {
        await _roomsRepository.RemoveAsync(notification.Room);
    }
}