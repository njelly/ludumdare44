using System.Collections;
using UnityEngine;

namespace Tofunaut.LudumDare44
{
    public class TowerBullet : MonoBehaviour
    {
        public float speed;
        public Vector3 target;

        private Vector3 _startPos;

        private void Start()
        {
            _startPos = gameObject.transform.position;

            if(target == Vector3.zero)
            {
                Debug.LogError("target is zero");
                Destroy(gameObject);
                return;
            }


        }

        private IEnumerator MoveCoroutine()
        {
            float progress = 0f;

            while(true)
            {
                progress += speed * Time.deltaTime;
                gameObject.transform.position = Vector3.Lerp(_startPos, target, progress);

                yield return null;
            }
        }
    }
}
