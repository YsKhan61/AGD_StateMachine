namespace StatePattern.StateMachine
{
    public interface IStateMachine
    {
        public void ChangeState(States newState);
    }

    public enum States
    {
        IDLE,
        ROTATING,
        SHOOTING,
        PATROLLING,
        CHASING
    }
}