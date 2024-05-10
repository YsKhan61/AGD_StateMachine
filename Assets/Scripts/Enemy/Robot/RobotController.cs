using ClassroomIGI.Player;
using ClassroomIGI.StateMachine;
using System.Collections.Generic;
using UnityEngine;


namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// The controller for the Robot Enemy
    /// </summary>
    public class RobotController : EnemyController, IRobotStateOwner
    {
        private RobotStateMachine stateMachine;

        public int CloneCountLeft { get; private set; }
        public float IdleDuration => data.IdleTime;
        public IList<Vector3> PatrollingPoints => data.PatrollingPoints;
        public float PlayerStoppingDistance => data.PlayerStoppingDistance;
        public float RotationSpeed => data.RotationSpeed;
        public float RotationThreshold => data.RotationThreshold;
        public float RateOfFire => data.RateOfFire;
        public Vector3 SpawnPosition => data.SpawnPosition;

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
            => base.PlayerEnteredRange(targetToSet);

        public override void PlayerExitedRange()
        {
            base.PlayerExitedRange();
            stateMachine.ChangeState(State.IDLE);
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

        public void OnTargetInView() => stateMachine.ChangeState(State.CHASING);
        public void OnTargetNotInView() => stateMachine.ChangeState(State.IDLE);
        public void OnIdleStateComplete() => stateMachine.ChangeState(State.PATROLLING);
        public void OnTeleportingStateComplete() => stateMachine.ChangeState(State.IDLE);
        public void OnPatrollingStateComplete() => stateMachine.ChangeState(State.IDLE);
        public void OnChasingStateComplete() => stateMachine.ChangeState(State.SHOOTING);

        private void CreateStateMachine() => stateMachine = new RobotStateMachine(this);
    }

}