
using ClassroomIGI.StateMachine;

namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// The state machine for the Titanis Enemy
    /// It will create the states for the Titanis
    /// The states are - Idle, Rotating, Roaring, Charge Attack, No Damage, Teleporting
    /// </summary>
    public class TitanisStateMachine : BaseStateMachine
    {
        public TitanisStateMachine(TitanisController owner)
        {
            CreateStates(owner);
        }

        private void CreateStates(TitanisController owner)
        {
            States.Add(State.IDLE, new IdleState(owner));
            States.Add(State.ROTATING, new RotatingState(owner));
            States.Add(State.ROARING, new RoaringState(owner));
            States.Add(State.CHARGE_ATTACK, new ChargeAttackState(owner));
            States.Add(State.NO_DAMAGE, new NoDamageState(owner));
            States.Add(State.TELEPORTING, new TeleportingState(owner));
        }
    }
}
