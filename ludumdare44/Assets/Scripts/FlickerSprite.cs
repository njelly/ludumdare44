using UnityEngine;

namespace Tofunaut.LudumDare44
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class FlickerSprite : MonoBehaviour
    {
        public float flickerInterval = 0.05f;

        private SpriteRenderer _spriteRenderer;
        private float _flickerTimer;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _flickerTimer = 0f;
        }

        private void Update()
        {
            _flickerTimer += Time.deltaTime;
            if(_flickerTimer > flickerInterval)
            {
                _spriteRenderer.enabled = !_spriteRenderer.enabled;
                _flickerTimer %= flickerInterval;
            }
        }
    }
}
