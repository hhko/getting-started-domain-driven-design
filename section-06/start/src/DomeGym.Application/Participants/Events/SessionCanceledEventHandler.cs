using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.Common.EventualConsistency;
using DomeGym.Domain.ParticipantAggregate;
using DomeGym.Domain.SessionAggregate.Events;

using MediatR;

namespace DomeGym.Application.Participants.Events.SessionCanceled;

public class SessionCanceledEventHandler : INotificationHandler<SessionCanceledEvent>
{
    private readonly IParticipantsRepository _participantsRepository;

    public SessionCanceledEventHandler(IParticipantsRepository participantsRepository)
    {
        _participantsRepository = participantsRepository;
    }

    public async Task Handle(SessionCanceledEvent notification, CancellationToken cancellationToken)
    {
        List<Participant> participants = await _participantsRepository.ListByIdsAsync(notification.Session.GetParticipantIds());

        participants.ForEach(participant =>
        {
            var removeFromScheduleResult = participant.RemoveFromSchedule(notification.Session);
            if (removeFromScheduleResult.IsError)
            {
                throw new EventualConsistencyException(
                    SessionCanceledEvent.ParticipantScheduleUpdateFailed,
                    removeFromScheduleResult.Errors);
            }
        });

        await _participantsRepository.UpdateRangeAsync(participants);
    }
}