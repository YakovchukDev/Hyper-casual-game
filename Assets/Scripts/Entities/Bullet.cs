using UnityEngine;
using DG.Tweening;

namespace Entities
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private const int _minNumberOfPoints = 10;
        private const int _maxNumberOfPoints = 30;
        private const float _minBulletSize = 0.3f;
        private int _numberOfPoints;
        private bool _isMove;

        private void FixedUpdate()
        {
            if(_isMove)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + _speed);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            
            if(other.gameObject.GetComponent<Obstacle>())
            {
                _isMove = false;
                Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, _minBulletSize * _numberOfPoints / 5f);
                foreach (var hitCollider in hitColliders)
                {
                    Obstacle obstacle = hitCollider.gameObject.GetComponent<Obstacle>();
                    if(obstacle != null)
                    {
                        obstacle.Infect();
                    }
                }
                this.gameObject.SetActive(false);
            }
        }
        public void SetBulletPower(int bulletPower)
        {
            print(bulletPower);
            _numberOfPoints = bulletPower;
            gameObject.transform.DOScale(_minBulletSize * _numberOfPoints / 15, 0);
        }
        public int GetMinNumberOfPoints() => _minNumberOfPoints;
        public int GetMaxNumberOfPoints() => _maxNumberOfPoints;
        public void Move()
        {
            _isMove = true;
        }
    }
}
