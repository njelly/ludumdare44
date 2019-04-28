using System.Collections;
using UnityEngine;

namespace Tofunaut.LudumDare44
{
    public class Tower : MonoBehaviour
    {
        public TowerBullet bulletPrefab;
        public float range;
        public float fireInterval;
        public int killCount;

        private void Start() 
        {
            StartCoroutine(fireCoroutine());
        }
        
        private IEnumerator fireCoroutine() 
        {
            while(true) 
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
