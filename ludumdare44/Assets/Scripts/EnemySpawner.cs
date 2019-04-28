using System.Collections;
using UnityEngine;

namespace Tofunaut.LudumDare44
{
    public class EnemySpawner : MonoBehaviour
    {
        public Enemy enemyPrefab;
        public float spawnEventInterval;
        public float spawnChance;
        public Transform spawnPosition;

        private Coroutine _spawnCoroutine;

        private void OnEnable()
        {
            if(_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
            }

            _spawnCoroutine = StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnEventInterval);

                TrySpawnEnemy();
            }
        }

        private void TrySpawnEnemy()
        {
            float roll = Random.value;
            if (roll < spawnChance)
            {
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            Enemy enemy = Instantiate(enemyPrefab);
            enemy.transform.position = spawnPosition.position;
        }
    }
}
