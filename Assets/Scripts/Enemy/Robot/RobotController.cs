using StatePattern.Player;
using StatePattern.StateMachine;


namespace StatePattern.Enemy
{
    public class RobotController : EnemyController
    {
        private RobotStateMachine stateMachine;
        public int CloneCountLeft { get; private set; }

        public RobotController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            SetCloneCount(enemyScriptableObject.CloneCount);
            enemyView.SetController(this);
            ChangeColor(EnemyColorType.Default);
            CreateStateMachine();
            stateMachine.ChangeState(State.IDLE);
        }

        public void SetCloneCount(int cloneCountToSet) => CloneCountLeft = cloneCountToSet;

        
        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;

            stateMachine.Update();

            enemyView.LogDebug($"Current State: {stateMachine.GetCurrentState() }");
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

        public override void Die()
        {
            if (CloneCountLeft > 0)
                stateMachine.ChangeState(State.CLONING);
            base.Die();
        }

        public void Teleport() => stateMachine.ChangeState(State.TELEPORTING);

        public void SetDefaultColor(EnemyColorType colorType) => enemyView.SetDefaultColor(colorType);

        public void ChangeColor(EnemyColorType colorType) => enemyView.ChangeColor(colorType);

        private void CreateStateMachine() => stateMachine = new RobotStateMachine(this);

    }

}