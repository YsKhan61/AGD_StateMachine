namespace ClassroomIGI.StateMachine
{
    /// <summary>
    /// An interface for the state of the state machine
    /// Any class implementing this interface will be a state
    /// </summary>
    public interface IState
    {
        public void OnStateEnter();
        public void Update();
        public void OnStateExit();
    }
}