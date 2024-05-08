using StatePattern.Player;
using StatePattern.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            stateMachine.ChangeState(States.IDLE);
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

        public override void Die()
        {
            if (CloneCountLeft > 0)
                stateMachine.ChangeState(States.CLONING);
            base.Die();
        }

        public void Teleport() => stateMachine.ChangeState(States.TELEPORTING);

        public void SetDefaultColor(EnemyColorType colorType) => enemyView.SetDefaultColor(colorType);

        public void ChangeColor(EnemyColorType colorType) => enemyView.ChangeColor(colorType);

        private void CreateStateMachine() => stateMachine = new RobotStateMachine(this);

    }

}