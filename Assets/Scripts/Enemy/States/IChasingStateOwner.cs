namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// An interface for the owner of the ChasingState
    /// An owner will chase the target
    /// It will have a NavMeshAgent to move towards the target
    /// It will chase the target until the target is not in view or the target is reached
    /// If the target is not in view, it will call OnTargetNotInView
    /// It will call OnChasingStateComplete when the target is reached
    /// </summary>
    public interface IChasingStateOwner : ITargetScanner
    {
        /// <summary>
        /// Get the stopping distance for the player
        /// </summary>
        public float PlayerStoppingDistance { get; }

        /// <summary>
        /// Get the NavMeshAgent of the owner
        /// </summary>
        public UnityEngine.AI.NavMeshAgent Agent { get; }

        /// <summary>
        /// Inform the owner that the chasing state is complete
        /// </summary>
        public void OnChasingStateComplete();
    }
}