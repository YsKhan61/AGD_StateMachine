using ClassroomIGI.Main;
using ClassroomIGI.Player;
using ClassroomIGI.StateMachine;
using UnityEngine;


namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// Charge attack state for the IChaseAttackStateOwner
    /// It will dash the owner straight to the player
    /// it will call TakeDamage on the player if owner hits the player
    /// When the owner reaches the player, it will call OnChargeAttackStateComplete
    /// </summary>
    public class ChargeAttackState : IState
    {
        private const float DASH_DURATION = 0.5f;
        private const float SPHERE_RADIUS = 0.5f;
        private const int CHARGE_DAMAGE = 20;

        private IChargeAttackStateOwner owner;
        private Vector3 targetPosition;
        private LayerMask playerLayer;

        public ChargeAttackState(IChargeAttackStateOwner owner) => this.owner = owner;

        public void OnStateEnter()
        {
            PlayerController player = GameService.Instance.PlayerService.GetPlayer();
            targetPosition = player.Position;
            playerLayer = GameService.Instance.PlayerLayer;

            owner.Agent.enabled = false;
        }

        public void OnStateExit() 
        {
            owner.Agent.enabled = true;
        }

        public void Update()
        {
            if (!owner.IsTargetInView())
            {
                owner.OnTargetNotInView();
            }

            TryChargeAttack();
        }

        private void TryChargeAttack()
        {
            Collider[] results = new Collider[1];
            owner.Transform.position = Vector3.Lerp(owner.Transform.position, targetPosition, (Time.deltaTime / DASH_DURATION));
            owner.Transform.rotation = Quaternion.LookRotation(targetPosition - owner.Transform.position);
            int hitCount = Physics.OverlapSphereNonAlloc(owner.Transform.position, SPHERE_RADIUS, results, playerLayer);
            if (hitCount <= 0) return;
            if (!results[0].TryGetComponent(out PlayerView player)) return;
            player.Controller.TakeDamage(CHARGE_DAMAGE);
            owner.OnChargeAttackStateComplete();
        }
    }
}