using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.GymAggregate.Events;

using MediatR;

namespace DomeGym.Application.Rooms.Events;

public class RoomAddedEventHandler : INotificationHandler<RoomAddedEvent>
{
    private readonly IRoomsRepository _roomsRepository;

    public RoomAddedEventHandler(IRoomsRepository roomsRepository)
    {
        _roomsRepository = roomsRepository;
    }

    public async Task Handle(RoomAddedEvent notification, CancellationToken cancellationToken)
    {
        await _roomsRepository.AddRoomAsync(notification.Room);
    }
}