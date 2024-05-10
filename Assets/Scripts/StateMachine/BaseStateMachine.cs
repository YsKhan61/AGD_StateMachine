using System.Collections.Generic;


namespace ClassroomIGI.StateMachine
{
    /// <summary>
    /// An abstract class for the state machine that contains the logic for changing states
    /// This is the base class for all the state machines of the game
    /// </summary>
    /// <typeparam name="T">The owner of the state machine</typeparam>
    public abstract class BaseStateMachine
    {
        protected IState currentState;
        protected Dictionary<State, IState> States = new Dictionary<State, IState>();

        /// <summary>
        /// Get the current state of the state machine
        /// </summary>
        /// <returns>the current state</returns>
        public IState GetCurrentState() => currentState;

        /// <summary>
        /// Update the current state of the state machine
        /// </summary>
        public void Update() => currentState?.Update();

        /// <summary>
        /// Change the state of the state machine
        /// </summary>
        /// <param name="newState"></param>
        public void ChangeState(State newState) => ChangeState(States[newState]);

        protected void ChangeState(IState newState)
        {
            currentState?.OnStateExit();
            currentState = newState;
            currentState?.OnStateEnter();
        }
    }
}