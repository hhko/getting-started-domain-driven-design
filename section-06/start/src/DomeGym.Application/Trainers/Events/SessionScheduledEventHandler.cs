using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.Common.EventualConsistency;
using DomeGym.Domain.RoomAggregate.Events;

using MediatR;

namespace DomeGym.Application.Trainers.Events;

public class SessionScheduledEventHandler : INotificationHandler<SessionScheduledEvent>
{
    private readonly ITrainersRepository _trainersRepository;

    public SessionScheduledEventHandler(ITrainersRepository trainersRepository)
    {
        _trainersRepository = trainersRepository;
    }

    public async Task Handle(SessionScheduledEvent notification, CancellationToken cancellationToken)
    {
        var trainer = await _trainersRepository.GetByIdAsync(notification.Session.TrainerId)
            ?? throw new EventualConsistencyException(SessionScheduledEvent.TrainerNotFound);

        var updateTrainerScheduleResult = trainer.AddSessionToSchedule(notification.Session);

        if (updateTrainerScheduleResult.IsError)
        {
            throw new EventualConsistencyException(
                SessionScheduledEvent.TrainerScheduleUpdateFailed,
                updateTrainerScheduleResult.Errors);
        }

        await _trainersRepository.UpdateAsync(trainer);
    }
}