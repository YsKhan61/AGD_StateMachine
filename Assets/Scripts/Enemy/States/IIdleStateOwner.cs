namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// An interface for the owner of the IdleState
    /// An owner will be in IdleState for a certain amount of time
    /// When the owner is in IdleState, it will check if the target is in view
    /// if the target is in view, it will call OnTargetInView
    /// it will call OnIdleStateComplete when the IdleState is complete
    /// </summary>
    public interface IIdleStateOwner : ITargetScanner
    {
        /// <summary>
        /// The duration for which the owner will be in IdleState
        /// </summary>
        public float IdleDuration { get; }
        
        /// <summary>
        /// Inform the owner that the IdleState is complete
        /// </summary>
        public void OnIdleStateComplete();
    }
}