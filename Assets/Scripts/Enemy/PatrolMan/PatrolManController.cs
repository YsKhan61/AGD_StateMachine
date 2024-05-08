using StatePattern.Player;
using StatePattern.StateMachine;


namespace StatePattern.Enemy
{
    public class PatrolManController : EnemyController
    {
        private PatrolManStateMachine stateMachine;

        public PatrolManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
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
            stateMachine.ChangeState(States.CHASING);
        }

        public override void OnTargetNotInView()
        {
            stateMachine.ChangeState(States.IDLE);
        }

        public override void OnIdleStateComplete()
        {
            stateMachine.ChangeState(States.PATROLLING);
        }

        private void CreateStateMachine() => stateMachine = new PatrolManStateMachine(this);
    }
}