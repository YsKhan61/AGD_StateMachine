using ClassroomIGI.StateMachine;


namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// The state machine for the One Punch Man Enemy
    /// It will create the states for the One Punch
    /// The states are - Idle, Rotating, Shooting
    /// </summary>
    public class OnePunchManStateMachine : BaseStateMachine
    {
        public OnePunchManStateMachine(OnePunchManController owner)
        {
            CreateStates(owner);
        }

        private void CreateStates(OnePunchManController owner)
        {
            States.Add(State.IDLE, new IdleState(owner));
            States.Add(State.ROTATING, new RotatingState(owner));
            States.Add(State.SHOOTING, new ShootingState(owner));
        }
    }
}