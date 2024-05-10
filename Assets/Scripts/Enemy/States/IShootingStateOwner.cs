using UnityEngine;


namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// An interface for the owner of the ShootingState
    /// An owner will scan for the target
    /// if target found, it will rotate towards the target
    /// and shoot at the target.
    /// if target is not in view, it will call OnTargetNotInView
    /// </summary>
    public interface IShootingStateOwner : ITargetScanner
    {
        /// <summary>
        /// Get the current position for the owner
        /// </summary>
        public Vector3 Position { get; }

        /// <summary>
        /// Get the current rotation for the owner
        /// </summary>
        public Quaternion Rotation { get; }

        /// <summary>
        /// Get the rotation speed for the owner
        /// </summary>
        public float RotationSpeed { get; }

        /// <summary>
        /// Get the threshold for the rotation to be considered complete
        /// </summary>
        public float RotationThreshold { get; }

        /// <summary>
        /// Get the rate of fire for the owner
        /// </summary>
        public float RateOfFire { get; }

        /// <summary>
        /// Set the rotation for the owner
        /// </summary>
        public void SetRotation(Quaternion desiredRotation);

        /// <summary>
        /// Shoot at the target
        /// </summary>
        public void Shoot();
    }
}