using StatePattern.StateMachine;
using System.Collections.Generic;
using UnityEngine;

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
            States.Add(StateMachine.State.IDLE, new IdleState<PatrolManController>(this));
            States.Add(StateMachine.State.PATROLLING, new PatrollingState<PatrolManController>(this));
            States.Add(StateMachine.State.CHASING, new ChasingState<PatrolManController>(this));
            States.Add(StateMachine.State.SHOOTING, new ShootingState<PatrolManController>(this));
        }
    }
}