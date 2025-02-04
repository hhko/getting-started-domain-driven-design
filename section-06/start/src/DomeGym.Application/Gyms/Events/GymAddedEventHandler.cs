using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.SubscriptionAggregate.Events;

using MediatR;

namespace DomeGym.Application.Gyms.Events;

public class GymAddedEventHandler : INotificationHandler<GymAddedEvent>
{
    private readonly IGymsRepository _gymsRepository;

    public GymAddedEventHandler(IGymsRepository gymsRepository)
    {
        _gymsRepository = gymsRepository;
    }

    public async Task Handle(GymAddedEvent notification, CancellationToken cancellationToken)
    {
        await _gymsRepository.AddGymAsync(notification.Gym);
    }
}