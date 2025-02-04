using DomeGym.Domain.Common;
using DomeGym.Domain.Common.EventualConsistency;
using DomeGym.Domain.SessionAggregate;

using ErrorOr;

namespace DomeGym.Domain.RoomAggregate.Events;

public record SessionScheduledEvent(Room Room, Session Session) : IDomainEvent
{
    public static readonly Error TrainerNotFound = EventualConsistencyError.From(
        code: "SessionScheduledEvent.TrainerNotFound",
        description: "Trainer not found");

    public static readonly Error TrainerScheduleUpdateFailed = EventualConsistencyError.From(
        code: "SessionScheduledEvent.TrainerScheduleUpdateFailed",
        description: "Adding session to trainer's schedule failed");

    public static readonly Error GymNotFound = EventualConsistencyError.From(
        code: "SessionScheduledEvent.GymNotFound",
        description: "Gym not found");
}