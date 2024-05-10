using ClassroomIGI.StateMachine;

namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// No Damage State for the INoDamageStateOwner
    /// It will set the owner to no damage state
    /// It will call OnNoDamageStateComplete when the no damage state is complete
    /// </summary>
    public class NoDamageState : IState
    {
        private const int DURATION = 2;
        private float timeElapsed;
        private INoDamageStateOwner owner;

        public NoDamageState(INoDamageStateOwner owner) => this.owner = owner;

        public void OnStateEnter()
        {
            timeElapsed = 0;
            owner.SetNoDamage(true);
        }

        public void OnStateExit()
        {
            owner.SetNoDamage(false);
        }

        public void Update()
        {
            if (timeElapsed >= DURATION)
            {
                owner.OnNoDamageStateComplete();
                return;
            }

            timeElapsed += UnityEngine.Time.deltaTime;
        }
    }
}