using UnityEngine;


namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// An interface for the owner of the RotatingState
    /// An owner will rotate to a target rotation
    /// It will check if the target is in view
    /// If the target is in view, it will call OnTargetInView
    /// It will call OnRotatingStateComplete when the rotation is complete
    /// </summary>
    public interface IRotatingStateOwner : ITargetScanner
    {
        /// <summary>
        /// Get the current rotation of the owner
        /// </summary>
        public Quaternion Rotation { get; }

        /// <summary>
        /// Get the speed at which the owner will rotate
        /// </summary>
        public float RotationSpeed { get; }

        /// <summary>
        /// Get the threshold for the rotation to be considered complete
        /// </summary>
        public float RotationThreshold { get; }

        /// <summary>
        /// Set the rotation of the owner
        /// </summary>
        /// <param name="rotation"></param>
        public void SetRotation(Vector3 rotation);

        /// <summary>
        /// Inform the owner that the rotating state is complete
        /// </summary>
        public void OnRotatingStateComplete();
    }
}