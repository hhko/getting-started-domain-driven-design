using DomeGym.Domain.Common;
using DomeGym.Domain.GymAggregate;

namespace DomeGym.Domain.SubscriptionAggregate.Events;

public record GymAddedEvent(Subscription Subscription, Gym Gym) : IDomainEvent;