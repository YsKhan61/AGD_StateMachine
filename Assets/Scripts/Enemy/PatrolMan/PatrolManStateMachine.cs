using ClassroomIGI.StateMachine;


namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// The state machine for the Patrol Man Enemy
    /// It will create the states for the Patrol
    /// The states are - Idle, Patrolling, Chasing, Shooting
    /// </summary>
    public class PatrolManStateMachine : BaseStateMachine
    {
        public string CurrentStateName => currentState.ToString();

        public PatrolManStateMachine(PatrolManController owner)
        {
            CreateStates(owner);
        }

        private void CreateStates(PatrolManController owner)
        {
            States.Add(State.IDLE, new IdleState(owner));
            States.Add(State.PATROLLING, new PatrollingState(owner));
            States.Add(State.CHASING, new ChasingState(owner));
            States.Add(State.SHOOTING, new ShootingState(owner));
        }
    }
}