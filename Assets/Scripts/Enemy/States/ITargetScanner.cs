namespace ClassroomIGI.Enemy
{
    public interface ITargetScanner
    {
        /// <summary>
        /// Is the target in view of the owner
        /// </summary>
        public bool IsTargetInView();

        /// <summary>
        /// Inform the owner that the target is in view
        /// </summary>
        public void OnTargetInView();

        /// <summary>
        /// Inform the owner that the target is not in view
        /// </summary>
        public void OnTargetNotInView();
    }
}