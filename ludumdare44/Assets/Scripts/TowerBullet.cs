using System.Collections;
using UnityEngine;

namespace Tofunaut.LudumDare44
{
    public class TowerBullet : MonoBehaviour
    {
        public float speed;
        public Vector3 target;
        public Tower source;

        private void Update()
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target, speed * Time.deltaTime);
            if((gameObject.transform.position - target).sqrMagnitude <= float.Epsilon)
            {
                Destroy(gameObject);
            }
        }
    }
}
