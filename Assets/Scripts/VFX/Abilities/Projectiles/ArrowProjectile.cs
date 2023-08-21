using UnityEngine;

namespace LuminaStudio.VFX.Abilities.Projectiles
{
    public class ArrowProjectile : MonoBehaviour
    {
        [SerializeField] 
        private TrailRenderer _trailRenderer;
        [SerializeField] 
        private Transform _hitVFX;
        private Vector3 _targetPosition;
        public void Setup(Vector3 targetPosition)
        {
            this._targetPosition = targetPosition;
        }

        private void Update()
        {
            var moveDir = (_targetPosition - transform.position).normalized;
            var distBefore = Vector3.Distance(transform.position, _targetPosition);
            transform.position += moveDir * 20f * Time.deltaTime;
            var distAfter = Vector3.Distance(transform.position, _targetPosition);

            // prevents overshoot when speed is too fast
            if (!(distBefore < distAfter)) return;
            transform.position = _targetPosition;
            _trailRenderer.transform.parent = null;
            Destroy(gameObject);
            Instantiate(_hitVFX, _targetPosition, Quaternion.identity);
        }
    }
}
