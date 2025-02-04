using DomeGym.Domain.Common;
using DomeGym.Domain.Common.EventualConsistency;

using ErrorOr;

namespace DomeGym.Domain.SessionAggregate.Events;

public record ReservationCanceledEvent(Session Session, Reservation Reservation) : IDomainEvent
{
    public static readonly Error ParticipantNotFound = EventualConsistencyError.From(
        code: "ReservationCanceledEvent.ParticipantNotFound",
        description: "Participant not found");

    public static readonly Error ParticipantScheduleUpdateFailed = EventualConsistencyError.From(
        code: "ReservationCanceledEvent.ParticipantScheduleUpdateFailed",
        description: "Removing session from participant schedule failed");
}