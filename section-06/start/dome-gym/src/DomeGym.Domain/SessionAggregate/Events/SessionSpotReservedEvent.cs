using DomeGym.Domain.Common;
using DomeGym.Domain.Common.EventualConsistency;

using ErrorOr;

namespace DomeGym.Domain.SessionAggregate.Events;

public record SessionSpotReservedEvent(Session Session, Reservation Reservation) : IDomainEvent
{
    public static readonly Error ParticipantScheduleUpdateFailed = EventualConsistencyError.From(
        code: "SessionSpotReserved.ParticipantScheduleUpdateFailed",
        description: "Adding session to participant schedule failed");
}