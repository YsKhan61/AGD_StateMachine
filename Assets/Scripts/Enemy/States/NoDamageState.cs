using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class NoDamageState<T> : IState where T : EnemyController
    {
        private const int DURATION = 2;
        private float timeElapsed;
        private GenericStateMachine<T> stateMachine;

        public EnemyController Owner { get; set; }

        public NoDamageState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            timeElapsed = 0;
            Owner.SetNoDamage(true);
        }

        public void OnStateExit()
        {
            Owner.SetNoDamage(false);
        }

        public void Update()
        {
            if (timeElapsed >= DURATION)
            {
                Owner.OnNoDamageStateComplete();
                return;
            }

            timeElapsed += UnityEngine.Time.deltaTime;
        }
    }
}