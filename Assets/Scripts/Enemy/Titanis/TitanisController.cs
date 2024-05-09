using StatePattern.StateMachine;
using static UnityEngine.UI.GridLayoutGroup;

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
            if (IsTargetInView())
            {
                stateMachine.ChangeState(State.ROARING);
            }
            else
            {
                stateMachine.ChangeState(State.IDLE);
            }
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

        public override void OnRoaringStateComplete()
        {
            if (IsTargetInView())
            {
                stateMachine.ChangeState(State.CHARGE_ATTACK);
            }
            else
            {
                stateMachine.ChangeState(State.IDLE);
            }
        }

        private void CreateStateMachine() => stateMachine = new TitanisStateMachine(this);
    }
}
