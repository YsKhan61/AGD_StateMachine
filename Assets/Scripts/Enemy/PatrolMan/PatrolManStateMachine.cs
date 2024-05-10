using StatePattern.StateMachine;


namespace StatePattern.Enemy
{
    public class PatrolManStateMachine : GenericStateMachine<PatrolManController>
    {
        public string CurrentStateName => currentState.ToString();

        public PatrolManStateMachine(PatrolManController Owner) : base(Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            States.Add(State.IDLE, new IdleState<PatrolManController>(this));
            States.Add(State.PATROLLING, new PatrollingState<PatrolManController>(this));
            States.Add(State.CHASING, new ChasingState<PatrolManController>(this));
            States.Add(State.SHOOTING, new ShootingState<PatrolManController>(this));
        }
    }
}