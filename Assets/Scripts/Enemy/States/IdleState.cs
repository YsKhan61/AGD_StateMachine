using ClassroomIGI.StateMachine;
using UnityEngine;

namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// Idle state of the IdleStateOwner
    /// It will wait for the duration of the IdleDuration and then change the state to the next state
    /// It will also check if the target is in view and change the state to the next state
    /// </summary>
    public class IdleState : IState
    {
        private IIdleStateOwner owner;
        private float timer;

        public IdleState(IIdleStateOwner owner) => this.owner = owner;

        public void OnStateEnter() => ResetTimer();

        public void Update()
        {
            if (owner.IsTargetInView())
            {
                owner.OnTargetInView();
                return;
            }

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                owner.OnIdleStateComplete();
            }
        }

        public void OnStateExit() => timer = 0;

        private void ResetTimer() => timer = owner.IdleDuration;
    }
}