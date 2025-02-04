using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.Common.EventualConsistency;
using DomeGym.Domain.RoomAggregate.Events;

using MediatR;

namespace DomeGym.Application.Gyms.Events;

public class SessionScheduledEventHandler : INotificationHandler<SessionScheduledEvent>
{
    private readonly IGymsRepository _gymsRepository;
    private readonly ITrainersRepository _trainersRepository;

    public SessionScheduledEventHandler(IGymsRepository gymsRepository, ITrainersRepository trainersRepository)
    {
        _gymsRepository = gymsRepository;
        _trainersRepository = trainersRepository;
    }

    public async Task Handle(SessionScheduledEvent notification, CancellationToken cancellationToken)
    {
        var gym = await _gymsRepository.GetByIdAsync(notification.Room.GymId)
            ?? throw new EventualConsistencyException(SessionScheduledEvent.GymNotFound);

        if (!gym.HasTrainer(notification.Session.TrainerId))
        {
            var trainer = await _trainersRepository.GetByIdAsync(notification.Session.TrainerId)
                ?? throw new EventualConsistencyException(SessionScheduledEvent.TrainerNotFound);

            gym.AddTrainer(trainer);
        }
    }
}