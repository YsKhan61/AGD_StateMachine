using ClassroomIGI.StateMachine;
using UnityEngine;


namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// Patrol state of the PatrolStateOwner
    /// It will patrol between a list of points
    /// It will check if the target is in view and change the state to the next state
    /// </summary>
    public class PatrollingState : IState
    {
        private IPatrolStateOwner owner;
        private int currentPatrollingIndex = -1;
        private Vector3 destination;

        public PatrollingState(IPatrolStateOwner owner) => this.owner = owner;

        public void OnStateEnter()
        {
            SetNextWaypointIndex();
            destination = GetDestination();
            MoveTowardsDestination();
        }

        public void Update()
        {
            if (owner.IsTargetInView())
            {
                owner.OnTargetInView();
                return;
            }

            if (ReachedDestination())
                owner.OnPatrollingStateComplete();
        }

        public void OnStateExit() { }

        private void SetNextWaypointIndex()
        {
            if (currentPatrollingIndex == owner.PatrollingPoints.Count-1)
                currentPatrollingIndex = 0;
            else
                currentPatrollingIndex++;
        }

        private Vector3 GetDestination() => owner.PatrollingPoints[currentPatrollingIndex];

        private void MoveTowardsDestination()
        {
            owner.Agent.isStopped = false;
            owner.Agent.SetDestination(destination);
        }

        private bool ReachedDestination() => owner.Agent.remainingDistance <= owner.Agent.stoppingDistance;

    }
}