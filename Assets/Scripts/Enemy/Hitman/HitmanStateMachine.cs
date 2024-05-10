using ClassroomIGI.StateMachine;

namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// The state machine for the Hitman Enemy
    /// It will create the states for the Hitman
    /// The states are - Idle, Patrolling, Chasing, Shooting, Teleporting
    /// </summary>
    public class HitmanStateMachine : BaseStateMachine
    {
        public HitmanStateMachine(HitmanController owner)
        {
            CreateStates(owner);
        }

        private void CreateStates(HitmanController owner)
        {
            States.Add(State.IDLE, new IdleState(owner));
            States.Add(State.PATROLLING, new PatrollingState(owner));
            States.Add(State.CHASING, new ChasingState(owner));
            States.Add(State.SHOOTING, new ShootingState(owner));
            States.Add(State.TELEPORTING, new TeleportingState(owner));
        }
    }
}