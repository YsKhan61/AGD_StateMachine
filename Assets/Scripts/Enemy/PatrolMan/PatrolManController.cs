using ClassroomIGI.Player;
using ClassroomIGI.StateMachine;
using System.Collections.Generic;
using UnityEngine;


namespace ClassroomIGI.Enemy
{

    /// <summary>
    /// The controller for the PatrolMan Enemy
    /// </summary>
    public class PatrolManController : EnemyController, IPatrolManStateOwner
    {
        private PatrolManStateMachine stateMachine;

        public float IdleDuration => data.IdleTime;
        public IList<Vector3> PatrollingPoints => data.PatrollingPoints;
        public float PlayerStoppingDistance => data.PlayerStoppingDistance;
        public float RotationSpeed => data.RotationSpeed;
        public float RotationThreshold => data.RotationThreshold;
        public float RateOfFire => data.RateOfFire;

        public PatrolManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
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

        public void OnTargetInView() => stateMachine.ChangeState(State.CHASING);
        public void OnTargetNotInView() => stateMachine.ChangeState(State.IDLE);
        public void OnIdleStateComplete() => stateMachine.ChangeState(State.PATROLLING);
        public void OnPatrollingStateComplete() => stateMachine.ChangeState(State.IDLE);
        public void OnChasingStateComplete() => stateMachine.ChangeState(State.SHOOTING);


        private void CreateStateMachine() => stateMachine = new PatrolManStateMachine(this);
    }
}