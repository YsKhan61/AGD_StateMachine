using StatePattern.Main;
using StatePattern.Player;
using StatePattern.StateMachine;
using UnityEngine;


namespace StatePattern.Enemy
{
    public class ChargeAttackState<T> : IState where T : EnemyController
    {
        private const float DASH_DURATION = 0.5f;
        private const float SPHERE_RADIUS = 0.5f;
        private const int CHARGE_DAMAGE = 20;

        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;
        private Vector3 targetPosition;
        private LayerMask playerLayer;
        private float timeElapsed;

        public ChargeAttackState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine; 

        public void OnStateEnter()
        {
            timeElapsed = 0f;

            PlayerController player = GameService.Instance.PlayerService.GetPlayer();
            targetPosition = player.Position;
            playerLayer = GameService.Instance.PlayerLayer;

            Owner.Agent.enabled = false;
        }

        public void OnStateExit() 
        {
            Owner.Agent.enabled = true;
        }

        public void Update()
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= DASH_DURATION)
            {
                stateMachine.ChangeState(State.IDLE);
                return;
            }

            TryChargeAttack();

            
        }

        private void TryChargeAttack()
        {
            Collider[] results = new Collider[1];
            Owner.Transform.position = Vector3.Lerp(Owner.Transform.position, targetPosition, (Time.deltaTime / DASH_DURATION));
            Owner.Transform.rotation = Quaternion.LookRotation(targetPosition - Owner.Position);
            int hitCount = Physics.OverlapSphereNonAlloc(Owner.Position, SPHERE_RADIUS, results, playerLayer);
            if (hitCount <= 0) return;
            if (!results[0].TryGetComponent(out PlayerView player)) return;
            player.Controller.TakeDamage(CHARGE_DAMAGE);
            stateMachine.ChangeState(State.IDLE);
        }
    }
}