using DomeGym.Domain.Common;
using DomeGym.Domain.RoomAggregate;

namespace DomeGym.Domain.GymAggregate.Events;

public record RoomRemovedEvent(Gym Gym, Room Room) : IDomainEvent;