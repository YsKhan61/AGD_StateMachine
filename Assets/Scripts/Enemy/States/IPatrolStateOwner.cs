using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// An interface for the owner of the PatrollingState
    /// An owner will patrol between a list of points
    /// It will check if the target is in view
    /// If the target is in view, it will call OnTargetInView
    /// It will call OnPatrollingStateComplete when the patrolling is complete
    /// </summary>
    public interface IPatrolStateOwner : ITargetScanner
    {
        /// <summary>
        /// Get the list of points that the owner will patrol between
        /// </summary>
        public IList<Vector3> PatrollingPoints { get; }

        /// <summary>
        /// Get the NavMeshAgent of the owner
        /// </summary>
        public NavMeshAgent Agent { get; }

        /// <summary>
        /// Inform the owner that the patrolling state is complete
        /// </summary>
        public void OnPatrollingStateComplete();
    }
}