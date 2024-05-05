using UnityEngine;

namespace StatePattern.Enemy
{
    public class PatrollingState : IState
    {
        public OnePunchManController Owner { get; set; }
        private OnePunchManStateMachine stateMachine;

        private int currentPatrollingIndex = -1;
        private Vector3 destination;

        public PatrollingState(OnePunchManStateMachine stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter() 
        {
            SetNextWaypointIndex();
            destination = GetDestination();
            MoveTowardsDestination();
        }

        public void Update() 
        {
            if (ReachedDestination())
                stateMachine.ChangeState(States.IDLE);
        }

        public void OnStateExit() { }

        private void SetNextWaypointIndex()
        {
            if (currentPatrollingIndex == Owner.Data.PatrollingPoints.Count - 1)
                currentPatrollingIndex = 0;
            else
                currentPatrollingIndex++;
        }

        private Vector3 GetDestination() => Owner.Data.PatrollingPoints[currentPatrollingIndex];

        private void MoveTowardsDestination()
        {
            Owner.Agent.isStopped = false;
            Owner.Agent.SetDestination(destination);
        }

        private bool ReachedDestination() => Owner.Agent.remainingDistance <= Owner.Agent.stoppingDistance;
    }
}