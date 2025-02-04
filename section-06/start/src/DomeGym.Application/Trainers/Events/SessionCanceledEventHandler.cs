using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.Common.EventualConsistency;
using DomeGym.Domain.SessionAggregate.Events;

using MediatR;

namespace DomeGym.Application.Trainers.Events;

public class SessionCanceledEventHandler : INotificationHandler<SessionCanceledEvent>
{
    private readonly ITrainersRepository _trainersRepository;

    public SessionCanceledEventHandler(ITrainersRepository trainersRepository)
    {
        _trainersRepository = trainersRepository;
    }

    public async Task Handle(SessionCanceledEvent notification, CancellationToken cancellationToken)
    {
        var trainer = await _trainersRepository.GetByIdAsync(notification.Session.TrainerId)
            ?? throw new EventualConsistencyException(SessionCanceledEvent.TrainerNotFound);

        var removeFromScheduleResult = trainer.RemoveFromSchedule(notification.Session);

        if (removeFromScheduleResult.IsError)
        {
            throw new EventualConsistencyException(
                SessionCanceledEvent.TrainerScheduleUpdateFailed,
                removeFromScheduleResult.Errors);
        }

        await _trainersRepository.UpdateAsync(trainer);
    }
}