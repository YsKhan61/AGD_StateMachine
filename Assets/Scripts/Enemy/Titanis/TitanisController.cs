using ClassroomIGI.StateMachine;
using UnityEngine;


namespace ClassroomIGI.Enemy
{

    /// <summary>
    /// The controller for the Titanis Enemy
    /// </summary>
    public class TitanisController : EnemyController, ITitanisStateOwner
    {
        private TitanisStateMachine stateMachine;

        public float IdleDuration => data.IdleTime;
        public float RotationSpeed => data.RotationSpeed;
        public float RotationThreshold => data.RotationThreshold;
        public Vector3 SpawnPosition => data.SpawnPosition;

        public TitanisController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
            CreateStateMachine();
            stateMachine.ChangeState(State.IDLE);
        }

        public void OnIdleStateComplete()
        {
            if (IsTargetInView())
            {
                stateMachine.ChangeState(State.ROARING);
            }
            else
            {
                stateMachine.ChangeState(State.ROTATING);
            }
        }

        public void OnTargetInView() => stateMachine.ChangeState(State.ROARING);

        public void OnTargetNotInView() => stateMachine.ChangeState(State.IDLE);

        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;

            stateMachine.Update();

            enemyView.LogDebug($"Current State: {stateMachine.GetCurrentState()}");
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            if (currentState == EnemyState.DEACTIVE)
                return;

            stateMachine.ChangeState(State.NO_DAMAGE);
        }

        public void OnRotatingStateComplete() => stateMachine.ChangeState(State.IDLE);

        public void OnRoaringStateComplete()
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

        public void OnNoDamageStateComplete() => stateMachine.ChangeState(State.TELEPORTING);
        public void OnTeleportingStateComplete() => stateMachine.ChangeState(State.IDLE);
        public void OnChargeAttackStateComplete() => stateMachine.ChangeState(State.NO_DAMAGE);

        private void CreateStateMachine() => stateMachine = new TitanisStateMachine(this);
    }
}
