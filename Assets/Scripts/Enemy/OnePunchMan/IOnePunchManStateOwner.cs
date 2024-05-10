namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// An interface containing all the state owners of the One Punch Man Enemy
    /// The interfaces are - IIdleStateOwner, IRotatingStateOwner, IShootingStateOwner
    /// </summary>
    public interface IOnePunchManStateOwner : IIdleStateOwner, IRotatingStateOwner, IShootingStateOwner
    {
    }
}