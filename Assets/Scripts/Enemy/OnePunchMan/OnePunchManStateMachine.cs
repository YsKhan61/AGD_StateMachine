using StatePattern.StateMachine;
using System.Collections.Generic;

namespace StatePattern.Enemy
{
    public class OnePunchManStateMachine : GenericStateMachine<OnePunchManController>
    {
        public OnePunchManStateMachine(OnePunchManController Owner) : base(Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            States.Add(StateMachine.State.IDLE, new IdleState<OnePunchManController>(this));
            States.Add(StateMachine.State.ROTATING, new RotatingState<OnePunchManController>(this));
            States.Add(StateMachine.State.SHOOTING, new ShootingState<OnePunchManController>(this));
        }
    }
}