using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace Entities
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _jumpForce = 1f;
        private int _pointsReserve;
        private int _maxPointsReserve;
        private int _minPointsReserve;
        private float _standardScale;

        private void Start()
        {
            _standardScale = gameObject.transform.localScale.x;
        }
        public void SetPointsReserve(int pointsReserve, int minPointsReserve)
        {
            _pointsReserve = pointsReserve;
            _maxPointsReserve = pointsReserve;
            _minPointsReserve = minPointsReserve;
        }
        public bool HasPoints(int numberOfPoints)
        {
            if (_pointsReserve - numberOfPoints >= _minPointsReserve)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool TakePoints(int numberOfPoints)
        {
            if (HasPoints(numberOfPoints))
            {
                _pointsReserve -= numberOfPoints;
                SizeRecalculation();
                return true;
            }
            else
            {
                return false;
            }
        }
        public void SizeRecalculation()
        {
            gameObject.transform.DOScale((float)_pointsReserve / _maxPointsReserve * _standardScale, 0.3f);
            gameObject.transform.DOMoveY(gameObject.transform.localScale.y / 4f, 0.3f);
        }
        public bool Move(float target)
        {
            Ray ray = new Ray(transform.position, new Vector3(0, 0, target));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<Obstacle>() != null)
                {
                    StartCoroutine(DoJump(hit.distance - 5));
                    return false;
                }
                else
                {
                    StartCoroutine(DoJump(target - gameObject.transform.localPosition.z - 3));
                    return true;
                }
            }
            return false;
        }
        public IEnumerator DoJump(float positionZ)
        {
            float positionY = gameObject.transform.position.y;
            gameObject.transform.DOMoveZ(gameObject.transform.position.z + positionZ, 0.5f);
            gameObject.transform.DOMoveY(_jumpForce, 0.25f);
            yield return new WaitForSeconds(0.25f);
            gameObject.transform.DOMoveY(positionY, 0.25f);
        }

    }
}
