using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tofunaut.LudumDare44
{
    public class GameController : MonoBehaviour
    {
        public static event EventHandler GameOver;

        private static GameController _instance;

        public static bool IsGameOver { get { return _instance._isGameOver; } }

        public static int Score
        {
            get
            {
                return _instance._score;
            }
            set
            {
                _instance._score = value;
                _instance.UpdateScoreLabel();
            }
        }

        public static int Health
        {
            get
            {
                return _instance._health;
            }
            set
            {
                _instance._health = value;

                if(_instance._health <= 0)
                {
                    _instance._health = 0;
                    _instance.OnGameOver();
                }

                _instance.UpdateHealthLabel();
            }
        }

        [Header("References")]
        public TMPro.TextMeshProUGUI healthLabel;
        public Image heartIcon;
        public TMPro.TextMeshProUGUI scoreLabel;
        public TMPro.TextMeshProUGUI gameOverLabel;
        public Tower towerPrefab;

        [Header("Config")]
        public int startHealth;
        public int towerCost = 1;

        private int _health;
        private int _score;
        private bool _isGameOver;
        private Plane _worldPlane;

        private void Awake()
        {
            if(_instance != null)
            {
                Debug.LogError("Only one GameController can exist at a time!");
                Destroy(this);
                return;
            }

            _instance = this;
            _isGameOver = false;
            _health = startHealth;

            Enemy.EnemyCompletedPath += Enemy_EnemyCompletedPath;

            _worldPlane = new Plane(Vector3.forward, 0f);
        }

        private void Start()
        {
            UpdateHealthLabel();
        }

        private void OnDestroy()
        {
            Enemy.EnemyCompletedPath -= Enemy_EnemyCompletedPath;
            _instance = null;
        }

        private void Update()
        {
            if(!IsGameOver && Input.GetMouseButtonDown(0))
            {
                TryPlaceTower();
            }
        }

        private void UpdateHealthLabel()
        {
            healthLabel.text = _health.ToString();

            iTween.PunchScale(heartIcon.gameObject, Vector3.one * 0.5f, 1f);
        }

        private void UpdateScoreLabel()
        {
            scoreLabel.text = string.Format("Score: {0}", _score);

            iTween.PunchScale(scoreLabel.gameObject, Vector3.one * 0.5f, 1f);
        }

        private void TryPlaceTower()
        {            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (_worldPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                
                hitPoint = new Vector3(Mathf.Round(hitPoint.x), Mathf.Round(hitPoint.y), hitPoint.z);

                Tower hitTower = null;
                foreach(Tower tower in Tower.Instances)
                {
                    float distanceSquared = (tower.gameObject.transform.position - hitPoint).sqrMagnitude;
                    if(distanceSquared <= float.Epsilon)
                    {
                        hitTower = tower;
                        break;
                    }
                }

                if(hitTower == null)
                {
                    Tower newTower = Instantiate(towerPrefab);
                    newTower.transform.position = hitPoint;
                    Health -= towerCost;
                }
                else
                {
                    Destroy(hitTower.gameObject);
                    Health += hitTower.killCount;
                }
            }
        }

        private void Enemy_EnemyCompletedPath(object sender, EnemyEventArgs e)
        {
            Debug.Log("enemy completed path");

            if(IsGameOver)
            {
                return;
            }

            Health -= 1;
        }

        private void OnGameOver()
        {
            _isGameOver = true;
            gameOverLabel.gameObject.SetActive(true);
            GameOver?.Invoke(this, EventArgs.Empty);
        }

        public void Restart()
        {
            // reload the game scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
