using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.Common.EventualConsistency;
using DomeGym.Domain.SessionAggregate.Events;

using MediatR;

namespace DomeGym.Application.Participants.Events.SessionCanceled;

public class ReservationCanceledEventHandler : INotificationHandler<ReservationCanceledEvent>
{
    private readonly IParticipantsRepository _participantsRepository;

    public ReservationCanceledEventHandler(IParticipantsRepository participantsRepository)
    {
        _participantsRepository = participantsRepository;
    }

    public async Task Handle(ReservationCanceledEvent notification, CancellationToken cancellationToken)
    {
        var participant = await _participantsRepository.GetByIdAsync(notification.Reservation.ParticipantId)
            ?? throw new EventualConsistencyException(ReservationCanceledEvent.ParticipantNotFound);

        var removeBookingResult = participant.RemoveFromSchedule(notification.Session);

        if (removeBookingResult.IsError)
        {
            throw new EventualConsistencyException(
                ReservationCanceledEvent.ParticipantScheduleUpdateFailed,
                removeBookingResult.Errors);
        }

        await _participantsRepository.UpdateAsync(participant);
    }
}