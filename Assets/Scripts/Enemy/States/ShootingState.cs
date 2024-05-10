using ClassroomIGI.Main;
using ClassroomIGI.Player;
using ClassroomIGI.StateMachine;
using UnityEngine;


namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// Shooting state of the ShootingStateOwner
    /// It will rotate towards the player and shoot at the player
    /// It will check if the player is in view
    /// if the player is not in view, it will call OnTargetNotInView
    /// </summary>
    public class ShootingState : IState
    {
        private IShootingStateOwner owner;
        private PlayerController target;
        private float shootTimer;

        public ShootingState(IShootingStateOwner owner) => this.owner = owner;

        public void OnStateEnter()
        {
            SetTarget();
            shootTimer = 0;
        }

        public void Update()
        {
            if (!owner.IsTargetInView())
            {
                owner.OnTargetNotInView();
                return;
            }

            Quaternion desiredRotation = CalculateRotationTowardsPlayer();
            owner.SetRotation(RotateTowards(desiredRotation));

            if(IsRotationComplete(desiredRotation))
            {
                shootTimer -= Time.deltaTime;
                if (shootTimer <= 0)
                {
                    ResetTimer();
                    owner.Shoot();
                }
            }
        }

        public void OnStateExit() => target = null;

        private void SetTarget() => target = GameService.Instance.PlayerService.GetPlayer();

        private Quaternion CalculateRotationTowardsPlayer()
        {
            Vector3 directionToPlayer = target.Position - owner.Position;
            directionToPlayer.y = 0f;
            return Quaternion.LookRotation(directionToPlayer, Vector3.up);
        }

        private Quaternion RotateTowards(Quaternion desiredRotation) => Quaternion.LerpUnclamped(owner.Rotation, desiredRotation, owner.RotationSpeed / 30 * Time.deltaTime);

        private bool IsRotationComplete(Quaternion desiredRotation) => Quaternion.Angle(owner.Rotation, desiredRotation) < owner.RotationThreshold;

        private void ResetTimer() => shootTimer = owner.RateOfFire;
    }
}