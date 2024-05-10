using ClassroomIGI.StateMachine;
using ClassroomIGI.Player;


namespace ClassroomIGI.Enemy
{

    /// <summary>
    /// The controller for the One Punch Man Enemy
    /// </summary>
    public class OnePunchManController : EnemyController, IOnePunchManStateOwner
    {
        public float IdleDuration => data.IdleTime;
        public float RotationSpeed => data.RotationSpeed;
        public float RotationThreshold => data.RotationThreshold;

        public float RateOfFire => data.RateOfFire;

        private OnePunchManStateMachine stateMachine;

        public OnePunchManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
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

        public override void PlayerEnteredRange(PlayerController targetToSet)
            => base.PlayerEnteredRange(targetToSet);

        public override void PlayerExitedRange()
        {
            base.PlayerExitedRange();
            stateMachine.ChangeState(State.IDLE);
        }

        public void OnTargetInView() => stateMachine.ChangeState(State.SHOOTING);
        public void OnTargetNotInView() => stateMachine.ChangeState(State.IDLE);
        public void OnIdleStateComplete() => stateMachine.ChangeState(State.ROTATING);
        public void OnRotatingStateComplete() => stateMachine.ChangeState(State.IDLE);

        private void CreateStateMachine() => stateMachine = new OnePunchManStateMachine(this);
    }
}