using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.UI;

namespace StatePattern.Enemy
{
    [System.Serializable]
    public struct EnemyColor
    {
        public EnemyColorType Type;
        public Color Color;
    }

    public enum EnemyColorType
    {
        Default,
        Vulnerable,
        Clone
    }

    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private EnemyTriggerBehaviour triggerBehaviour;
        [SerializeField] public NavMeshAgent Agent;
        [SerializeField] private SpriteRenderer enemyGraphic;
        [SerializeField] private SpriteRenderer detectableRange;
        [SerializeField] private ParticleSystem muzzleFlash;
        [SerializeField] private List<EnemyColor> enemyColors;
        [SerializeField] private GameObject bloodStain;

        private SphereCollider rangeTriggerCollider;

        public EnemyController Controller { get; private set; }
        private Slider healthBar;

        [Header("Debug Only")]
        [SerializeField, TextArea(5, 5)]
        private string debugMessage;

        private void Start()
        {

            rangeTriggerCollider = GetComponent<SphereCollider>();
            Controller?.InitializeAgent();
        }

        private void Update() => Controller?.UpdateEnemy();

        public void SetController(EnemyController controllerToSet)
        {
            Controller = controllerToSet;
            triggerBehaviour.SetController(controllerToSet);        // considering every trigger 
        }

        public void InitializeHealthBar(GameObject healthBarToSet)
        {
            GameObject healthBarParent = Instantiate(healthBarToSet);
            healthBar = healthBarParent.GetComponentInChildren<Slider>();
            PositionConstraint constraint = healthBarParent.AddComponent<PositionConstraint>();
            constraint.AddSource(new ConstraintSource { sourceTransform = transform, weight = 1 });
            constraint.translationOffset = new Vector3(0, 1, 2.5f);
            constraint.constraintActive = true;
        }

        public void SetTriggerRadius(float radiusToSet)
        {
            SetRangeColliderRadius(radiusToSet);
            SetRangeImageRadius(radiusToSet);
        }

        public void PlayShootingEffect() => muzzleFlash.Play();

        public void Destroy() => StartCoroutine(EnemyDeathSequence());

        public void ChangeColor(EnemyColorType colorType) => enemyGraphic.color = enemyColors.Find(item => item.Type == colorType).Color;

        public void SetDefaultColor(EnemyColorType colorType)
        {
            EnemyColor coloToSetAsDefault = new EnemyColor();
            coloToSetAsDefault.Type = EnemyColorType.Default;
            coloToSetAsDefault.Color = enemyColors.Find(item => item.Type == colorType).Color;

            enemyColors.Remove(enemyColors.Find(item => item.Type == EnemyColorType.Default));
            enemyColors.Add(coloToSetAsDefault);
        }

        public void UpdateHealthBar(float healthPercentage) => healthBar.value = healthPercentage;

        public void LogDebug(string message) => debugMessage = message;

        private void SetRangeImageRadius(float radiusToSet) => detectableRange.transform.localScale = new Vector3(radiusToSet, radiusToSet, 1);

        private IEnumerator EnemyDeathSequence()
        {
            Controller.ToggleKillOverlay(true);
            Controller.ShakeCamera();

            yield return new WaitForSeconds(0.1f);

            var blood = Instantiate(bloodStain);
            blood.transform.position = transform.position;
            Controller.ToggleKillOverlay(false);

            Destroy(gameObject);
        }

        private void SetRangeColliderRadius(float radiusToSet)
        {
            if (rangeTriggerCollider != null)
                rangeTriggerCollider.radius = radiusToSet;
        }

        private void OnDrawGizmos() => Controller?.DrawGizmos();
    }
}