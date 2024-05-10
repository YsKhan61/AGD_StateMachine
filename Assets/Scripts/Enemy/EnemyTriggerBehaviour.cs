using ClassroomIGI.Player;
using UnityEngine;

namespace ClassroomIGI.Enemy
{
    public class EnemyTriggerBehaviour : MonoBehaviour
    {
        public EnemyController Controller { get; private set; }

        public void SetController(EnemyController controllerToSet) => Controller = controllerToSet;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.isTrigger || !other.TryGetComponent(out PlayerView playerView)) return;
            Controller.PlayerEnteredRange(playerView.Controller);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.isTrigger || !other.TryGetComponent(out PlayerView playerView)) return;
            Controller.PlayerExitedRange();
        }
    }
}