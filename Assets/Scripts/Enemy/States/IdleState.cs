using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class IdleState<T> : IState where T : EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;
        private float timer;

        public IdleState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter() => ResetTimer();

        public void Update()
        {
            if (Owner.IsTargetInView())
            {
                Owner.OnTargetInView();
                return;
            }

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Owner.OnIdleStateComplete();
            }
        }

        public void OnStateExit() => timer = 0;

        private void ResetTimer() => timer = Owner.Data.IdleTime;
    }
}