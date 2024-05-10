namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// An interface for the CloningState owner
    /// It will create a clone of the owner
    /// No. of clones will be based on the Clone count of the owner's data
    /// </summary>
    public interface ICloningStateOwner
    {
        /// <summary>
        /// Get the data of the owner
        /// </summary>
        public EnemyScriptableObject Data { get; }
    }
}