using UnityEngine;
using UnityEngine.AI;

namespace ClassroomIGI.Enemy
{
    /// <summary>
    ///  An interface for the owner of the TeleportingState
    ///  It will teleport to a random position
    ///  Then it will call OnTeleportingStateComplete
    /// </summary>
    public interface ITeleportingStateOwner
    {
        /// <summary>
        /// Get the position of the owner
        /// </summary>
        public Vector3 Position { get; }

        /// <summary>
        /// Get the NavMeshAgent of the owner
        /// </summary>
        public NavMeshAgent Agent { get; }

        /// <summary>
        /// Get the spawn position of the owner
        /// </summary>
        public Vector3 SpawnPosition { get; }

        /// <summary>
        /// Inform the owner that the teleporting state is complete
        /// </summary>
        public void OnTeleportingStateComplete();

    }
}