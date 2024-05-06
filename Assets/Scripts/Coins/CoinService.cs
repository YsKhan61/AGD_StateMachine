using StatePattern.Main;
using System.Collections.Generic;
using UnityEngine;


namespace StatePattern.Coin
{
    public class CoinService
    {
        private CoinView coinPrefab;
        private LayerMask obstacleLayer;

        public CoinService(CoinView coinPrefab)
        {
            this.coinPrefab = coinPrefab;
            obstacleLayer = GameService.Instance.ObstacleLayer;
            GameService.Instance.EventService.OnEnemyDead.AddListener(OnEnemyDead);
        }

        ~CoinService()
        {
            GameService.Instance.EventService.OnEnemyDead.RemoveListener(OnEnemyDead);
        }

        private void OnEnemyDead(Vector3 deathPosition)
        {
            SpawnCoins(deathPosition);
        }

        private void SpawnCoins(Vector3 center)
        {
            if (!TryGetPositions(center, out List<Vector3> positions))
            {
                return;
            }

            foreach (Vector3 position in positions)
            {
                Object.Instantiate(coinPrefab, position, Quaternion.identity);
            }
        }

        private bool TryGetPositions(Vector3 center, out List<Vector3> positions)
        {
            positions = new List<Vector3>();

            int maxTries = 10;
            int maxCoins = 4;
            int spawnCount = 0;
            int radius = 4;

            for (int i = 0; i < maxTries; i++)
            {
                Vector2 random = Random.insideUnitCircle;
                Vector3 position = center + new Vector3(random.x, 0, random.y) * radius;
                if (IsPositionValid(position))
                {
                    positions.Add(position);
                    spawnCount++;
                }

                if (spawnCount >= maxCoins)
                {
                    break;
                }
            }

            return positions.Count > 0;
        }

        private bool IsPositionValid(Vector3 position)
        {
            Debug.Log("Coin position: " + position);
            Collider[] _ = new Collider[1];
            if (Physics.OverlapSphereNonAlloc(position, 0.5f, _, obstacleLayer) > 0)
            {
                // Debug.Log("Not valid");
                // return false;
                return false;
            }

            return true;
        }
    }
}