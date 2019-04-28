using UnityEngine;
using UnityEngine.UI;

namespace Tofunaut.LudumDare44
{
    public class GameController : MonoBehaviour
    {
        public int Health
        {
            get
            {
                return _health;
            }
            set
            {
                _health = value;
                UpdateHealthLabel();
            }
        }

        [Header("References")]
        public TMPro.TextMeshProUGUI healthLabel;
        public Image heartIcon;

        [Header("Config")]
        public int startHealth;

        private int _health;

        private void Awake()
        {
            Enemy.EnemyCompletedPath += Enemy_EnemyCompletedPath;
        }

        private void Start()
        {
            _health = startHealth;
            UpdateHealthLabel();
        }

        private void UpdateHealthLabel()
        {
            healthLabel.text = _health.ToString();

            iTween.PunchScale(heartIcon.gameObject, Vector3.one * 0.5f, 1f);
        }

        private void Enemy_EnemyCompletedPath(object sender, EnemyEventArgs e)
        {
            _health -= 1;
            if (Health <= 0)
            {
                _health = 0;
                Debug.Log("Game Over");
            }

            UpdateHealthLabel();
        }


    }
}
