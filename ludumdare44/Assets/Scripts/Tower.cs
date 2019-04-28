using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tofunaut.LudumDare44
{
    public class Tower : MonoBehaviour
    {
        public static List<Tower> Instances = new List<Tower>();

        public TowerBullet bulletPrefab;
        public float range;
        public float fireInterval;
        public int killCount;

        private void Start() 
        {
            StartCoroutine(fireCoroutine());
            Instances.Add(this);
        }

        private void OnDestroy()
        {
            Instances.Remove(this);
        }

        private IEnumerator fireCoroutine() 
        {
            while(!GameController.IsGameOver)
            {
                yield return new WaitForSeconds(fireInterval);

                tryFireBullet();
            }
        }
        
        private void tryFireBullet() 
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            float minDistanceSquared = range * range;
            Enemy nearestEnemy = null;
            foreach(Enemy enemy in enemies) 
            {
                float distanceSquared = (enemy.gameObject.transform.position - gameObject.transform.position).sqrMagnitude;
                if(distanceSquared < minDistanceSquared) 
                {
                    nearestEnemy = enemy;
                    minDistanceSquared = distanceSquared;
                }
            }
            
            if(nearestEnemy == null) 
            {
                return;
            }
            
            
            TowerBullet bullet = Instantiate(bulletPrefab);
            bullet.transform.position = gameObject.transform.position;
            bullet.target = nearestEnemy.gameObject.transform.position;
            bullet.source = this;
        }
    }
}
