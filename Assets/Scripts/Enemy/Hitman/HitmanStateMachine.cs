using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class HitmanStateMachine : GenericStateMachine<HitmanController>
    {
        public HitmanStateMachine(HitmanController Owner) : base(Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            States.Add(StateMachine.State.IDLE, new IdleState<HitmanController>(this));
            States.Add(StateMachine.State.PATROLLING, new PatrollingState<HitmanController>(this));
            States.Add(StateMachine.State.CHASING, new ChasingState<HitmanController>(this));
            States.Add(StateMachine.State.SHOOTING, new ShootingState<HitmanController>(this));
            States.Add(StateMachine.State.TELEPORTING, new TeleportingState<HitmanController>(this));
        }
    }
}