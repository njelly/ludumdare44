﻿using Dreamteck.Splines;
using System;
using UnityEngine;

namespace Tofunaut.LudumDare44
{
    public class Enemy : MonoBehaviour
    {
        public static event EventHandler<EnemyEventArgs> EnemyCompletedPath;

        public int health;
        public float pathSpeed;

        private SplineFollower _splineFollower;

        private void Start()
        {
            _splineFollower = GetComponent<SplineFollower>();
            _splineFollower.computer = FindObjectOfType<SplineComputer>();
            _splineFollower.followSpeed = pathSpeed;

            GameController.GameOver += GameController_GameOver;
        }

        private void OnDestroy()
        {
            GameController.GameOver -= GameController_GameOver;
        }

        private void Update()
        {
            if(GameController.IsGameOver)
            {
                return;
            }

            if (_splineFollower.clampedPercent >= 1)
            {
                EnemyCompletedPath?.Invoke(this, new EnemyEventArgs(this));
                Destroy(gameObject);
            }
        }
        
        public void OnTriggerEnter2D(Collider2D col)
        {
            TowerBullet bullet = col.gameObject.GetComponent<TowerBullet>();
            if(bullet == null) {
                return;
            }

            health -= 1;
            if(health <= 0) 
            {
                Destroy(gameObject);
                bullet.source.killCount++;

                GameController.Score += 1;
            }

            Destroy(bullet.gameObject);
        }

        private void GameController_GameOver(object sender, EventArgs e)
        {
            _splineFollower.enabled = false;
        }
    }

    public class EnemyEventArgs : EventArgs
    {
        readonly public Enemy enemy;

        public EnemyEventArgs(Enemy enemy)
        {
            this.enemy = enemy;
        }
    }
}
