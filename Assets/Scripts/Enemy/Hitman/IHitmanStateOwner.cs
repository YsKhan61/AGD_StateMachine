namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// An interface containing all the state owner interfaces of the Hitman Enemy
    /// The interfaces are - IIdleStateOwner, IPatrolStateOwner, IChasingStateOwner, IShootingStateOwner, ITeleportingStateOwner
    /// </summary>
    public interface IHitmanStateOwner : IIdleStateOwner, IPatrolStateOwner, IChasingStateOwner, IShootingStateOwner, ITeleportingStateOwner { }
}