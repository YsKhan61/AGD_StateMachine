using StatePattern.Enemy.Bullet;
using StatePattern.Main;
using StatePattern.Player;
using UnityEngine;
using UnityEngine.AI;

namespace StatePattern.Enemy
{
    public class EnemyController
    {
        protected EnemyScriptableObject enemyScriptableObject;
        protected EnemyView enemyView;

        protected int currentHealth;
        protected EnemyState currentState;
        public NavMeshAgent Agent => enemyView.Agent;
        public EnemyScriptableObject Data => enemyScriptableObject;
        public Quaternion Rotation => enemyView.transform.rotation;
        public Vector3 Position => enemyView.transform.position;

        protected PlayerController playerInRange;

        /// <summary>
        /// The layer to search for obstacles between player and enemy
        /// </summary>
        private LayerMask raycastLayer;

        public EnemyController(EnemyScriptableObject enemyScriptableObject)
        {
            this.enemyScriptableObject = enemyScriptableObject;
            InitializeView();
            InitializeVariables();
        }

        private void InitializeView()
        {
            enemyView = Object.Instantiate(enemyScriptableObject.EnemyPrefab);
            enemyView.transform.position = enemyScriptableObject.SpawnPosition;
            enemyView.transform.rotation = Quaternion.Euler(enemyScriptableObject.SpawnRotation);
            enemyView.SetTriggerRadius(enemyScriptableObject.RangeRadius);
        }

        private void InitializeVariables()
        {
            SetState(EnemyState.ACTIVE);
            currentHealth = enemyScriptableObject.MaximumHealth;
            raycastLayer = GameService.Instance.ObstacleLayer | GameService.Instance.PlayerLayer;
        }

        public void InitializeAgent()
        {
            Agent.enabled = true;
            Agent.SetDestination(enemyScriptableObject.SpawnPosition);
            Agent.speed = enemyScriptableObject.MovementSpeed;
        }

        public virtual void Die() 
        {
            GameService.Instance.EnemyService.EnemyDied(this);
            enemyView.Destroy();
        }

        public void ToggleKillOverlay(bool value) => GameService.Instance.UIService.ToggleKillOverlay(value);

        public void ShakeCamera() => GameService.Instance.UIService.ShakeCamera();

        public void SetRotation(Vector3 eulerAngles) => enemyView.transform.rotation = Quaternion.Euler(eulerAngles);

        public void SetRotation(Quaternion desiredRotation) => enemyView.transform.rotation = desiredRotation;

        public void ToggleEnemyColor(EnemyColorType colorToSet) => enemyView.ChangeColor(colorToSet);

        public virtual void Shoot()
        {
            enemyView.PlayShootingEffect();
            GameService.Instance.SoundService.PlaySoundEffects(Sound.SoundType.ENEMY_SHOOT);
            BulletController bullet = new BulletController(enemyView.transform, enemyScriptableObject.BulletData);
        }

        public void SetState(EnemyState stateToSet) => currentState = stateToSet;

        public virtual void PlayerEnteredRange(PlayerController targetToSet)
        {
            playerInRange = targetToSet;
            GameService.Instance.SoundService.PlaySoundEffects(Sound.SoundType.ENEMY_ALERT);
        }

        public virtual void PlayerExitedRange() 
        {
            playerInRange = null;
        }

        public virtual void UpdateEnemy() { }

        public bool IsTargetInView()
        {
            if (playerInRange == null)
                return false;

            // try a capsule cast from enemy to player to check if there is any obstacle in between
            RaycastHit hit;
            bool isHit = Physics.CapsuleCast(
                enemyView.transform.position,
                enemyView.transform.position + enemyView.transform.forward * 0.1f,  // capsule cast height
                0.2f,       // capsule cast radius 
                playerInRange.Position - enemyView.transform.position, 
                out hit, 
                enemyScriptableObject.RangeRadius, 
                raycastLayer);

            if (!isHit)
            {
                return false;
            }

            if ((1 << hit.collider.gameObject.layer) != GameService.Instance.PlayerLayer)
            {
                return false;
            }

            Vector3 direction = (playerInRange.Position - enemyView.transform.position).normalized;
            float angle = Vector3.Angle(direction, enemyView.transform.forward);

            return angle < enemyScriptableObject.FOV;
        }

        public virtual void OnTargetInView() { }

        public virtual void OnTargetNotInView() { }

        public virtual void OnIdleStateComplete() { }

        public void DrawGizmos()
        {
            DrawDetectableRange();
            DrawPlayerDetectionWithRaycast();

            void DrawDetectableRange()
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(enemyView.transform.position, enemyScriptableObject.RangeRadius);
            }

            void DrawPlayerDetectionWithRaycast()
            {
                if (playerInRange == null)
                    return;

                Gizmos.color = Color.green;
                Gizmos.DrawRay(
                    enemyView.transform.position,
                    (playerInRange.Position - enemyView.transform.position).normalized
                    * enemyScriptableObject.RangeRadius);
            }
        }
    }

    public enum EnemyState
    {
        ACTIVE,
        DEACTIVE
    }
}