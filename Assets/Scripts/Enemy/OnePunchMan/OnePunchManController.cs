using StatePattern.StateMachine;
using StatePattern.Player;

namespace StatePattern.Enemy
{
    public class OnePunchManController : EnemyController
    {
        private OnePunchManStateMachine stateMachine;

        public OnePunchManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
            CreateStateMachine();
            stateMachine.ChangeState(States.IDLE);
        }

        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;

            stateMachine.Update();

            enemyView.LogDebug($"Current State: {stateMachine.GetCurrentState()}");
        }

        public override void PlayerEnteredRange(PlayerController targetToSet)
        {
            base.PlayerEnteredRange(targetToSet);
        }

        public override void PlayerExitedRange()
        {
            base.PlayerExitedRange();
            stateMachine.ChangeState(States.IDLE);
        }

        public override void OnTargetInView()
        {
            stateMachine.ChangeState(States.SHOOTING);
        }

        public override void OnTargetNotInView()
        {
            stateMachine.ChangeState(States.IDLE);
        }

        public override void OnIdleStateComplete()
        {
            stateMachine.ChangeState(States.ROTATING);
        }

        private void CreateStateMachine() => stateMachine = new OnePunchManStateMachine(this);
    }
}