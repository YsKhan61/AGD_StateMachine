using StatePattern.StateMachine;


namespace StatePattern.Enemy
{
    public class RobotStateMachine : GenericStateMachine<RobotController>
    {
        public RobotStateMachine(RobotController Owner) : base(Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            States.Add(State.IDLE, new IdleState<RobotController>(this));
            States.Add(State.PATROLLING, new PatrollingState<RobotController>(this));
            States.Add(State.CHASING, new ChasingState<RobotController>(this));
            States.Add(State.SHOOTING, new ShootingState<RobotController>(this));
            States.Add(State.TELEPORTING, new TeleportingState<RobotController>(this));
            States.Add(State.CLONING, new CloningState<RobotController>(this));
        }
    }
}