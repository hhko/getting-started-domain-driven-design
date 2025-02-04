using GymManagement.Domain.GymAggregate;
using GymManagement.Domain.RoomAggregate;
using GymManagement.Domain.UnitTests.TestConstants;

namespace GymManagement.Domain.UnitTests.TestUtils.Rooms;

public static class RoomFactory
{
    public static Room CreateRoom(
        int maxDailySessions = Constants.Room.MaxDailySessions,
        Guid? gymId = null,
        Guid? id = null)
    {
        return new Room(
            name: Constants.Room.Name,
            maxDailySessions: maxDailySessions,
            gymId: gymId ?? Constants.Gym.Id,
            id: id ?? Constants.Room.Id);
    }
}