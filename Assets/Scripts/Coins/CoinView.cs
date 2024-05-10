using ClassroomIGI.Main;
using ClassroomIGI.Player;
using System.Collections;
using UnityEngine;


namespace ClassroomIGI.Coin
{
    [RequireComponent(typeof(Rigidbody))]
    public class CoinView : MonoBehaviour
    {
        private const float MOVE_SPEED = 0.1f;
        private const float TARGET_DISTANCE_THRESHOLD = 0.1f;

        [SerializeField]
        private int coinValue = 10;

        private Transform target;
        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out PlayerView playerView))
            {
                return;
            }

            rb.detectCollisions = false;

            GameService.Instance.EventService.OnCoinCollected.InvokeEvent(coinValue);
            target = playerView.transform;


            StartCoroutine(MoveToPlayer());
        }

        private IEnumerator MoveToPlayer()
        {
            while (Vector3.SqrMagnitude(target.position - transform.position) > TARGET_DISTANCE_THRESHOLD)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, MOVE_SPEED);
                yield return null;
            }

            Destroy(gameObject);
        }
    }


}