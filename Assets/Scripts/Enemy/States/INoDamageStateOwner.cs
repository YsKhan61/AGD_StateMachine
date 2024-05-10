namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// An interface for the NoDamageStateOwner
    /// it will set the owner to no damage state
    /// It will call OnNoDamageStateComplete when the no damage state is complete
    /// </summary>
    public interface INoDamageStateOwner
    {
        /// <summary>
        /// Set the owner to no damage state
        /// </summary>
        /// <param name="isNoDamage">if true, owner will not take any damage, false otherwise</param>
        public void SetNoDamage(bool isNoDamage);

        /// <summary>
        /// Inform the owner that the no damage state is complete
        /// </summary>
        public void OnNoDamageStateComplete();
    }
}