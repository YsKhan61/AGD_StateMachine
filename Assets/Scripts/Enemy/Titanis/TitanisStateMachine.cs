
using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class TitanisStateMachine : GenericStateMachine<TitanisController>
    {
        public TitanisStateMachine(TitanisController Owner) : base(Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            States.Add(State.IDLE, new IdleState<TitanisController>(this));
            States.Add(State.ROTATING, new RotatingState<TitanisController>(this));
            States.Add(State.ROARING, new RoaringState<TitanisController>(this));
            States.Add(State.CHARGE_ATTACK, new ChargeAttackState<TitanisController>(this));
            States.Add(State.NO_DAMAGE, new NoDamageState<TitanisController>(this));
            States.Add(State.TELEPORTING, new TeleportingState<TitanisController>(this));
        }
    }
}
