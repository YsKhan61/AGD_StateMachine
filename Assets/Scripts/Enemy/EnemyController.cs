using ClassroomIGI.Enemy.Bullet;
using ClassroomIGI.Main;
using ClassroomIGI.Player;
using UnityEngine;
using UnityEngine.AI;

namespace ClassroomIGI.Enemy
{
    public abstract class EnemyController
    {
        protected EnemyScriptableObject data;
        protected EnemyView enemyView;

        protected int currentHealth;
        protected EnemyState currentState;
        public NavMeshAgent Agent => enemyView.Agent;
        public EnemyScriptableObject Data => data;
        public Quaternion Rotation => enemyView.transform.rotation;
        public Vector3 Position => enemyView.transform.position;
        public Transform Transform => enemyView.transform;   

        protected PlayerController playerInRange;

        /// <summary>
        /// The layer to search for obstacles between player and enemy
        /// </summary>
        private LayerMask raycastLayer;

        private bool noDamage;

        public EnemyController(EnemyScriptableObject enemyScriptableObject)
        {
            this.data = enemyScriptableObject;
            InitializeView();
            InitializeVariables();
        }

        private void InitializeView()
        {
            enemyView = Object.Instantiate(data.EnemyPrefab);
            enemyView.transform.position = data.SpawnPosition;
            enemyView.transform.rotation = Quaternion.Euler(data.SpawnRotation);
            enemyView.SetTriggerRadius(data.RangeRadius);
            enemyView.InitializeHealthBar(data.HealthBarPrefab);
        }

        private void InitializeVariables()
        {
            SetState(EnemyState.ACTIVE);
            currentHealth = data.MaximumHealth;
            raycastLayer = GameService.Instance.ObstacleLayer | GameService.Instance.PlayerLayer;
        }

        public void InitializeAgent()
        {
            Agent.enabled = true;
            Agent.SetDestination(data.SpawnPosition);
            Agent.speed = data.MovementSpeed;
        }

        public virtual void TakeDamage(int damage) 
        {
            if (noDamage)
                return;

            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }

            enemyView.UpdateHealthBar((float)currentHealth / data.MaximumHealth);
        }

        public virtual void Die()
        {
            GameService.Instance.EnemyService.EnemyDied(this);
            enemyView.Destroy();
        }

        public virtual void SetNoDamage(bool value) => noDamage = value;

        public void ToggleKillOverlay(bool value) => GameService.Instance.UIService.ToggleKillOverlay(value);

        public void ShakeCamera() => GameService.Instance.UIService.ShakeCamera();

        public void SetRotation(Vector3 eulerAngles) => enemyView.transform.rotation = Quaternion.Euler(eulerAngles);

        public void SetRotation(Quaternion desiredRotation) => enemyView.transform.rotation = desiredRotation;

        public void ToggleEnemyColor(EnemyColorType colorToSet) => enemyView.ChangeColor(colorToSet);

        public virtual void Shoot()
        {
            enemyView.PlayShootingEffect();
            GameService.Instance.SoundService.PlaySoundEffects(Sound.SoundType.ENEMY_SHOOT);
            new BulletController(enemyView.transform, data.BulletData);
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

        public abstract void UpdateEnemy();

        public virtual bool IsTargetInView()
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
                data.RangeRadius, 
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

            return angle < data.FOV;
        }

        public void DrawGizmos()
        {
            DrawDetectableRange();
            DrawPlayerDetectionWithRaycast();

            void DrawDetectableRange()
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(enemyView.transform.position, data.RangeRadius);
            }

            void DrawPlayerDetectionWithRaycast()
            {
                if (playerInRange == null)
                    return;

                Gizmos.color = Color.green;
                Gizmos.DrawRay(
                    enemyView.transform.position,
                    (playerInRange.Position - enemyView.transform.position).normalized
                    * data.RangeRadius);
            }
        }
    }

    public enum EnemyState
    {
        ACTIVE,
        DEACTIVE
    }
}