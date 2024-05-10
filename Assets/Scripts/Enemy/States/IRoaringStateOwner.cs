namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// An interface for the RoaringStateOwner
    /// It will invoke OnRoaringStateComplete when the roaring state is complete
    /// It will invoke the event OnEnemyRoar
    /// </summary>
    public interface IRoaringStateOwner
    {
        /// <summary>
        /// Inform the owner that the roaring state is complete
        /// </summary>
        public void OnRoaringStateComplete();
    }
}