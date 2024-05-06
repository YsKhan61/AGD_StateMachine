using StatePattern.Player;
using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class CloneManController : EnemyController
    {
        private CloneManStateMachine stateMachine;
        public int CloneCountLeft { get; private set; }

        public CloneManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            SetCloneCount(enemyScriptableObject.CloneCount);
            enemyView.SetController(this);
            ChangeColor(EnemyColorType.Default);
            CreateStateMachine();
            stateMachine.ChangeState(States.IDLE);
        }

        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;

            stateMachine.Update();
        }

        public override void PlayerEnteredRange(PlayerController targetToSet)
        {
            base.PlayerEnteredRange(targetToSet);
            stateMachine.ChangeState(States.CHASING);
        }

        public override void PlayerExitedRange() => stateMachine.ChangeState(States.IDLE);

        public override void Die()
        {
            if (CloneCountLeft > 0)
                stateMachine.ChangeState(States.CLONING);
            base.Die();
        }

        public void Teleport() => stateMachine.ChangeState(States.TELEPORTING);

        public void SetDefaultColor(EnemyColorType colorType) => enemyView.SetDefaultColor(colorType);

        public void ChangeColor(EnemyColorType colorType) => enemyView.ChangeColor(colorType);

        public void SetCloneCount(int cloneCountToSet) => CloneCountLeft = cloneCountToSet;

        private void CreateStateMachine() => stateMachine = new CloneManStateMachine(this);
    }
}