using ClassroomIGI.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassroomIGI.Enemy.Bullet
{
    public class BulletView : MonoBehaviour
    {
        private BulletController controller;

        public void SetController(BulletController controlleroSet) => controller = controlleroSet;

        private void Update() => controller.UpdateBullet();

        private void OnTriggerEnter(Collider other)
        {
            if(HasHitPlayer(other))
            {
                if (other.isTrigger)
                    return;
                else 
                    controller.PlayerHit(other.GetComponent<PlayerView>());
            }
            Destroy(gameObject);
        }

        private bool HasHitPlayer(Collider other) => other.GetComponent<PlayerView>() != null;
    }
}