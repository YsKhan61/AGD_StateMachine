using UnityEngine;
using UnityEngine.AI;


namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// An interface for the owner of the ChargeAttackState
    /// It will dash straight to the player
    /// If hits the player, it will call TakeDamage on the player
    /// On reaching the player, it will call OnChargeAttackStateComplete
    /// </summary>
    public interface IChargeAttackStateOwner
    {
        /// <summary>
        /// Get the transform of the owner
        /// </summary>
        public Transform Transform { get; }

        /// <summary>
        /// Get the NavMeshAgent of the owner
        /// </summary>
        public NavMeshAgent Agent { get; }

        /// <summary>
        /// Inform the owner that the charge attack state is complete
        /// </summary>
        public void OnChargeAttackStateComplete();
    }
}