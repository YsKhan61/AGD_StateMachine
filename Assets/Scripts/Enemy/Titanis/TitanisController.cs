using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class TitanisController : EnemyController
    {
        private TitanisStateMachine stateMachine;

        public TitanisController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
            CreateStateMachine();
            stateMachine.ChangeState(StateMachine.State.IDLE);
        }

        public override void OnIdleStateComplete()
        {
            stateMachine.ChangeState(StateMachine.State.IDLE);
        }

        public override void OnTargetInView()
        {
            stateMachine.ChangeState(State.ROARING);
        }

        public override void OnTargetNotInView()
        {
            stateMachine.ChangeState(State.IDLE);
        }

        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;

            stateMachine.Update();

            enemyView.LogDebug($"Current State: {stateMachine.GetCurrentState()}");
        }

        private void CreateStateMachine() => stateMachine = new TitanisStateMachine(this);
    }
}
