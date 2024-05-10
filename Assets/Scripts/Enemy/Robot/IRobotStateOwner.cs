namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// An interface containing all the state owners of the robot enemy
    /// The interfaces are - IIdleStateOwner, IPatrolStateOwner, IChasingStateOwner, IShootingStateOwner, ITeleportingStateOwner, ICloningStateOwner
    /// </summary>
    public interface IRobotStateOwner : IIdleStateOwner, IPatrolStateOwner, IChasingStateOwner, IShootingStateOwner, ITeleportingStateOwner, ICloningStateOwner
    {
    }

}