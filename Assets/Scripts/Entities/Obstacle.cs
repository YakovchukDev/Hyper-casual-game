using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace Entities
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private Material _material;
        private MeshRenderer _meshRenderer;
        private const float _lifeTimeOfInfected = 1f;
        private void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }
        public void Infect()
        {
            StartCoroutine(GetInfected());
        }
        public float GetLifeTimeOfInfected() => _lifeTimeOfInfected;
        private IEnumerator GetInfected()
        {
            _meshRenderer.material.DOColor(_material.color, 0.5f);
            yield return new WaitForSeconds(_lifeTimeOfInfected);
            Destroy(this.gameObject);
        }
    }
}
