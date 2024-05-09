using StatePattern.Player;
using StatePattern.StateMachine;


namespace StatePattern.Enemy
{
    public class HitmanController : EnemyController
    {
        private HitmanStateMachine stateMachine;

        public HitmanController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
            CreateStateMachine();
            stateMachine.ChangeState(State.IDLE);
        }

        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;

            stateMachine.Update();

            enemyView.LogDebug($"Current State: {stateMachine.GetCurrentState()}");
        }

        public override void Shoot()
        {
            base.Shoot();
            stateMachine.ChangeState(State.TELEPORTING);
        }

        public override void PlayerEnteredRange(PlayerController targetToSet)
        {
            base.PlayerEnteredRange(targetToSet);
        }

        public override void PlayerExitedRange()
        {
            base.PlayerExitedRange();
            stateMachine.ChangeState(State.IDLE);
        }

        public override void OnTargetInView()
        {
            stateMachine.ChangeState(State.CHASING);
        }

        public override void OnTargetNotInView()
        {
            stateMachine.ChangeState(State.IDLE);
        }

        public override void OnIdleStateComplete()
        {
            stateMachine.ChangeState(State.PATROLLING);
        }

        private void CreateStateMachine() => stateMachine = new HitmanStateMachine(this);
    }
}