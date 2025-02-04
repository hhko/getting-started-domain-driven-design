namespace GymManagement.Domain.UnitTests.TestConstants;

public static partial class Constants
{
    public static class Room
    {
        public static readonly Guid Id = Guid.NewGuid();
        public const int MaxDailySessions = Subscriptions.MaxSessionsFreeTier;
        public const string Name = "Room 1";
    }
}