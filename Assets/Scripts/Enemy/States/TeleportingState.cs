using StatePattern.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace StatePattern.Enemy
{
    public class TeleportingState<T> : IState where T : EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;

        public TeleportingState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            TeleportToRandomPosition();
            stateMachine.ChangeState(States.CHASING);
        }

        public void Update() { }
        public void OnStateExit() { }

        private void TeleportToRandomPosition() => Owner.Agent.Warp(GetRandomNavMeshPoint());

        private Vector3 GetRandomNavMeshPoint()
        {
            Vector3 randomDirection = Random.insideUnitSphere * Owner.Data.TeleportingRadius + Owner.Position;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomDirection, out hit, Owner.Data.TeleportingRadius, NavMesh.AllAreas))
                return hit.position;

            return Owner.Data.SpawnPosition;
        }
    }
}