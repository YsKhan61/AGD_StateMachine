using ClassroomIGI.StateMachine;


namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// The state machine for the Robot Enemy
    /// It will create the states for the Robot
    /// The states are - Idle, Patrolling, Chasing, Shooting, Teleporting, Cloning
    /// </summary>
    public class RobotStateMachine : BaseStateMachine
    {
        public RobotStateMachine(RobotController owner)
        {
            CreateStates(owner);
        }

        private void CreateStates(RobotController owner)
        {
            States.Add(State.IDLE, new IdleState(owner));
            States.Add(State.PATROLLING, new PatrollingState(owner));
            States.Add(State.CHASING, new ChasingState(owner));
            States.Add(State.SHOOTING, new ShootingState(owner));
            States.Add(State.TELEPORTING, new TeleportingState(owner));
            States.Add(State.CLONING, new CloningState(owner));
        }
    }
}