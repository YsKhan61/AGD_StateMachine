using StatePattern.Enemy;
using System.Collections.Generic;

namespace StatePattern.StateMachine
{
    public class GenericStateMachine<T> where T : EnemyController
    {
        protected T Owner;
        protected IState currentState;
        protected Dictionary<States, IState> States = new Dictionary<States, IState>();

        public GenericStateMachine(T Owner) => this.Owner = Owner;

        public void Update() => currentState?.Update();

        public void ChangeState(States newState) => ChangeState(States[newState]);

        protected void ChangeState(IState newState)
        {
            currentState?.OnStateExit();
            currentState = newState;
            currentState?.OnStateEnter();
        }
        protected void SetOwner()
        {
            foreach (IState state in States.Values)
            {
                state.Owner = Owner;
            }
        }
    }

}