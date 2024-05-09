using StatePattern.Main;
using StatePattern.Player;
using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class RoaringState<T> : IState where T : EnemyController
    {
        private const int DURATION = 2;             // This state will stay for 2 seconds

        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;
        private PlayerController target;
        private float timeElapsed;      // time elapsed since entered the state.

        public RoaringState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            GameService.Instance.EventService.OnEnemyRoar.InvokeEvent(0);
            SetTarget();
            target.ModifiyMovementSpeed(0.5f);          // reduce player movement speed by half
            timeElapsed = 0;
        }

        public void OnStateExit()
        {
            target.ModifiyMovementSpeed(1f);            // reset the player movement speed by default.
            target = null;
        }

        public void Update()
        {
            if (timeElapsed >= DURATION)
            {
                if (Owner.IsTargetInView())
                {
                    stateMachine.ChangeState(State.SHOOTING);
                }
                else
                {
                    stateMachine.ChangeState(State.IDLE);
                }

                return;
            }

            timeElapsed += Time.deltaTime;
        }

        private void SetTarget() => target = GameService.Instance.PlayerService.GetPlayer();
    }
}