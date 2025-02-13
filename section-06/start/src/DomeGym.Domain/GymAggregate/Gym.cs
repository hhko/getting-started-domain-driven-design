using DomeGym.Domain.Common;
using DomeGym.Domain.GymAggregate.Events;
using DomeGym.Domain.RoomAggregate;
using DomeGym.Domain.TrainerAggregate;

using ErrorOr;

namespace DomeGym.Domain.GymAggregate;

public class Gym : AggregateRoot
{
    private readonly int _maxRooms;
    private readonly List<Guid> _roomIds = [];
    private readonly List<Guid> _trainerIds = [];

    public string Name { get; } = null!;

    public IReadOnlyList<Guid> RoomIds => _roomIds;

    public Guid SubscriptionId { get; }

    public Gym(
        string name,
        int maxRooms,
        Guid subscriptionId,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        Name = name;
        _maxRooms = maxRooms;
        SubscriptionId = subscriptionId;
    }

    public ErrorOr<Success> AddRoom(Room room)
    {
        if (_roomIds.Contains(room.Id))
        {
            return Error.Conflict(description: "Room already exists in gym");
        }

        if (_roomIds.Count >= _maxRooms)
        {
            return GymErrors.CannotHaveMoreRoomsThanSubscriptionAllows;
        }

        _roomIds.Add(room.Id);

        _domainEvents.Add(new RoomAddedEvent(this, room));

        return Result.Success;
    }
    public bool HasRoom(Guid roomId)
    {
        return _roomIds.Contains(roomId);
    }

    public ErrorOr<Success> AddTrainer(Trainer trainer)
    {
        if (_trainerIds.Contains(trainer.Id))
        {
            return Error.Conflict(description: "Trainer already assigned to gym");
        }

        _trainerIds.Add(trainer.Id);

        return Result.Success;
    }

    public bool HasTrainer(Guid trainerId)
    {
        return _trainerIds.Contains(trainerId);
    }

    public ErrorOr<Success> RemoveRoom(Room room)
    {
        if (!_roomIds.Contains(room.Id))
        {
            return Error.NotFound(description: "Room not found");
        }

        _roomIds.Remove(room.Id);

        _domainEvents.Add(new RoomRemovedEvent(this, room));

        return Result.Success;
    }

    private Gym() { }
}
