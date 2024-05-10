namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// An interface containing all the state owners of the PatrolMan Enemy
    /// The interfaces are - IIdleStateOwner, IPatrolStateOwner, IChasingStateOwner, IShootingStateOwner
    /// </summary>
    public interface IPatrolManStateOwner : IIdleStateOwner, IPatrolStateOwner, IChasingStateOwner, IShootingStateOwner
    {
    }
}