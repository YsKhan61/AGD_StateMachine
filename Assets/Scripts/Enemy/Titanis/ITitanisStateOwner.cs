namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// An interface containing all the state owners of the Titanis Enemy
    /// The interfaces are - IIdleStateOwner, IRotatingStateOwner, IRoaringStateOwner, IChargeAttackStateOwner, INoDamageStateOwner, ITeleportingStateOwner
    /// </summary>
    public interface ITitanisStateOwner : IIdleStateOwner, IRotatingStateOwner, IRoaringStateOwner, IChargeAttackStateOwner, INoDamageStateOwner, ITeleportingStateOwner
    {
    }
}
