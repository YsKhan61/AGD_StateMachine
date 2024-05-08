using StatePattern.Main;
using System.Collections.Generic;
using UnityEngine;


namespace StatePattern.Coin
{
    public class CoinService
    {
        /// <summary>
        /// Maximum no of tries to spawn coins
        /// </summary>
        private const int MAX_TRIES = 30;

        /// <summary>
        /// Maximum no of coins to spawn
        /// </summary>
        private const int MAX_COINS = 4;

        private CoinView coinPrefab;
        private LayerMask obstacleLayer;
        private LayerMask playerLayer;
        private LayerMask coinLayer;
        private Vector2 radiusRange = new Vector2(2f, 5f);
        private int spawnCount = 0;

        public CoinService(CoinView coinPrefab)
        {
            this.coinPrefab = coinPrefab;
            obstacleLayer = GameService.Instance.ObstacleLayer;
            playerLayer = GameService.Instance.PlayerLayer;
            coinLayer = GameService.Instance.CoinLayer;
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

            int tried = 0;
            spawnCount = 0;

            for (int i = 0; i < MAX_TRIES; i++)
            {
                tried++;

                Vector2 random = Random.insideUnitCircle;
                Vector3 position = center + new Vector3(random.x, 0, random.y) * Random.Range(radiusRange.x, radiusRange.y);
                if (IsPositionValid(position))
                {
                    positions.Add(position);
                    spawnCount++;
                }

                if (spawnCount >= MAX_COINS)
                {
                    break;
                }
            }
            return positions.Count > 0;
        }

        private bool IsPositionValid(Vector3 position)
        {
            Collider[] _ = new Collider[1];

            // combine all layers
            LayerMask allLayers = obstacleLayer | playerLayer | coinLayer;

            if (Physics.OverlapSphereNonAlloc(position, 0.5f, _, allLayers) > 0)
            {
                return false;
            }

            return true;
        }
    }
}