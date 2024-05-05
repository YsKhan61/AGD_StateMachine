﻿using StatePattern.Enemy;
using UnityEngine;

namespace StatePattern.StateMachine
{
    public class IdleState : IState
    {
        public OnePunchManController Owner { get; set; }
        private OnePunchManStateMachine stateMachine;
        private float timer;

        public IdleState(OnePunchManStateMachine stateMachine) => this.stateMachine = stateMachine;


        public void OnStateEnter() => ResetTimer();

        public void Update() 
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                stateMachine.ChangeState(OnePunchManStates.ROTATING);
        }

        public void OnStateExit() => timer = 0;

        private void ResetTimer() => timer = Owner.Data.IdleTime;
    }
}