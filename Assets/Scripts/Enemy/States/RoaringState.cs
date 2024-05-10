using ClassroomIGI.Main;
using ClassroomIGI.Player;
using ClassroomIGI.StateMachine;
using UnityEngine;

namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// Roaring state for the IRoaringStateOwner
    /// It will reduce the player movement speed by half
    /// It will invoke the event OnEnemyRoar
    /// It will call OnRoaringStateComplete when the roaring is complete
    /// </summary>
    public class RoaringState : IState
    {
        private const int DURATION = 2;             // This state will stay for 2 seconds

        private IRoaringStateOwner owner;
        private PlayerController target;
        private float timeElapsed;      // time elapsed since entered the state.

        public RoaringState(IRoaringStateOwner owner) => this.owner = owner;

        public void OnStateEnter()
        {
            GameService.Instance.EventService.OnEnemyRoar.InvokeEvent(0);
            SetTarget();
            target.ModifiyMovementSpeed(0.5f);          // reduce player movement speed by half
            timeElapsed = 0;
        }

        public void OnStateExit()
        {
            target.ModifiyMovementSpeed(1f);            // reset the player movement speed by default.
            target = null;
        }

        public void Update()
        {
            if (timeElapsed >= DURATION)
            {
                owner.OnRoaringStateComplete();
                return;
            }

            timeElapsed += Time.deltaTime;
        }

        private void SetTarget() => target = GameService.Instance.PlayerService.GetPlayer();
    }
}