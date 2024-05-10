using ClassroomIGI.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// Teleporting state for the ITeleportingStateOwner
    /// It will teleport to a random position
    /// It will call OnTeleportingStateComplete when the teleport is complete
    /// </summary>
    public class TeleportingState : IState
    {
        private ITeleportingStateOwner owner;

        public TeleportingState(ITeleportingStateOwner owner) => this.owner = owner;

        public void OnStateEnter()
        {
            TeleportToRandomPosition();
            owner.OnTeleportingStateComplete();
        }

        public void Update() { }

        public void OnStateExit() { }

        private void TeleportToRandomPosition() => owner.Agent.Warp(GetRandomNavMeshPoint());

        private Vector3 GetRandomNavMeshPoint()
        {
            Vector3 randomDirection = Random.insideUnitSphere * 5f + owner.Position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, 5f, NavMesh.AllAreas))
                return hit.position;
            else
                return owner.SpawnPosition;
        }
    }
}