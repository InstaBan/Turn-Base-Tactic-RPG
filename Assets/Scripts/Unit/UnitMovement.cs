using UnityEngine;
using LuminaStudio.Core.InputSystem;

namespace LuminaStudio.Unit
{
    public class UnitMovement : MonoBehaviour
    {
        #region attributes
        private Animator _animator;
        private float _stoppingDistance = 0.1f;
        [SerializeField]
        private float _movementSpeed = 5f;
        private float _distanceToDestination;
        private Vector3 _destination;
        #endregion

        private void Awake() // replace with OnstartClient when networked
        {
            //base.OnStartClient();

            // replace value with values readed from scriptable objects later
            _animator = GetComponent<Animator>();
        }
        private void Update()
        {
            _distanceToDestination = Vector3.Distance(transform.position, _destination);
            if (_distanceToDestination >= _stoppingDistance)
            {
                var direction = (_destination - transform.position).normalized;
                transform.forward = Vector3.Lerp(transform.forward, direction, Time.deltaTime * 10f);
                transform.position += direction * _movementSpeed * Time.deltaTime;
            }
            else
            {
                _animator.SetBool("isMoving", false);
            }
            // replace with event maybe?
            if (Input.GetMouseButtonDown(0))
            {
                SetDestination();
                _animator.SetBool("isMoving", true);
            }
        }

        private void SetDestination()
        {
            _destination = InputManager.GetMousePosition();
        }
        private void SetDestination(Vector3 destination)
        {
            _destination = destination;
        }
    }
}