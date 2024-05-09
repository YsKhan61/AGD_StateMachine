using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class RobotStateMachine : GenericStateMachine<RobotController>
    {
        public RobotStateMachine(RobotController Owner) : base(Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            States.Add(StateMachine.State.IDLE, new IdleState<RobotController>(this));
            States.Add(StateMachine.State.PATROLLING, new PatrollingState<RobotController>(this));
            States.Add(StateMachine.State.CHASING, new ChasingState<RobotController>(this));
            States.Add(StateMachine.State.SHOOTING, new ShootingState<RobotController>(this));
            States.Add(StateMachine.State.TELEPORTING, new TeleportingState<RobotController>(this));
            States.Add(StateMachine.State.CLONING, new CloningState<RobotController>(this));
        }
    }
}