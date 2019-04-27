using Dreamteck.Splines;
using UnityEngine;

namespace Tofunaut.LudumDare44
{
    public class Enemy : MonoBehaviour
    {
        public int health;
        public float pathSpeed;

        private SplineFollower _splineFollower;

        private void Start()
        {
            _splineFollower = GetComponent<SplineFollower>();
            _splineFollower.computer = FindObjectOfType<SplineComputer>();
            _splineFollower.followSpeed = pathSpeed;
        }
    }
}
