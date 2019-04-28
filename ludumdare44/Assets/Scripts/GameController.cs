using UnityEngine;
using UnityEngine.UI;

namespace Tofunaut.LudumDare44
{
    public class GameController : MonoBehaviour
    {
        [Header("References")]
        public TMPro.TextMeshProUGUI healthLabel;
        public Image heartIcon;

        [Header("Config")]
        public int health;

        private void Awake()
        {
            Enemy.EnemyCompletedPath += Enemy_EnemyCompletedPath;
        }

        private void Start()
        {
            UpdateHealthLabel();
        }

        private void UpdateHealthLabel()
        {
            healthLabel.text = health.ToString();

            iTween.PunchScale(heartIcon.gameObject, Vector3.one * 0.5f, 1f);
        }

        private void Enemy_EnemyCompletedPath(object sender, EnemyEventArgs e)
        {
            health -= 1;
            if (health <= 0)
            {
                health = 0;
                Debug.Log("Game Over");
            }

            UpdateHealthLabel();
        }


    }
}
