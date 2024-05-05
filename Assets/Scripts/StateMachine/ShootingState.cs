using StatePattern.Enemy;
using UnityEngine;

namespace StatePattern.StateMachine
{
    public class ShootingState : IState
    {
        public OnePunchManController Owner { get; set; }
        private OnePunchManStateMachine stateMachine;
        private float timeSinceLastShot;
        
        public ShootingState(OnePunchManStateMachine stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter() => Owner.Shoot();
        public void Update() 
        { 
            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot >= Owner.Data.RateOfFire)
                Owner.Shoot();
        }
        public void OnStateExit() => timeSinceLastShot = 0;
    }
}