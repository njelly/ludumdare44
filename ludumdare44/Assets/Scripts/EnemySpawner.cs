using System;
using System.Collections;
using UnityEngine;

namespace Tofunaut.LudumDare44
{
    public class EnemySpawner : MonoBehaviour
    {
        public EnemySpawnWeightPair[] spawnWeightPairs;
        public float spawnChance;
        public float spawnEventInterval;
        public float spawnIntervalDecreaseRate;
        public Transform spawnPosition;

        private Coroutine _spawnCoroutine;
        private float _weightSum;

        private void Start()
        {
            if(_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
            }

            _weightSum = 0f;
            foreach(EnemySpawnWeightPair pair in spawnWeightPairs)
            {
                _weightSum += pair.weight;
            }

            _spawnCoroutine = StartCoroutine(Spawn());
        }

        private void Update()
        {
            spawnEventInterval -= Time.deltaTime * spawnIntervalDecreaseRate;
            spawnEventInterval = Mathf.Max(0.1f, spawnEventInterval);
        }

        private IEnumerator Spawn()
        {
            while (!GameController.IsGameOver)
            {
                TrySpawnEnemy();

                yield return new WaitForSeconds(spawnEventInterval);
            }
        }

        private void TrySpawnEnemy()
        {
            if(spawnChance < UnityEngine.Random.value)
            {
                return;
            }

            float roll = UnityEngine.Random.value * _weightSum;
            float weightCounter = 0f;
            for(int pairIdx = 0; pairIdx < spawnWeightPairs.Length; pairIdx++)
            {
                weightCounter += spawnWeightPairs[pairIdx].weight;
                if(roll <= weightCounter)
                {
                    SpawnEnemy(spawnWeightPairs[pairIdx].prefab);
                    break;
                }
            }
        }

        private void SpawnEnemy(Enemy enemyPrefab)
        {
            Enemy enemy = Instantiate(enemyPrefab);
            enemy.transform.position = spawnPosition.position;
        }

        [Serializable]
        public struct EnemySpawnWeightPair
        {
            public float weight;
            public Enemy prefab;
        }
    }
}
