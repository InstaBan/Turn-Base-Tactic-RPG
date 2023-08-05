using UnityEngine;
using LuminaStudio.Calculation.Logic;
using LuminaStudio.Core.Input;
using LuminaStudio.Grid;

namespace LuminaStudio.Unit
{
    public class UnitBase : MonoBehaviour
    {
        #region attributes
        private Animator _animator;
        private float _stoppingDistance = 0.1f;
        [SerializeField]
        private float _movementSpeed = 5f;
        private float _distanceToDestination;
        private Vector3 _destination;
        private GridPosition _gridPosition;
        #endregion

        private void Awake() // replace with OnstartClient when networked
        {
            //base.OnStartClient();

            // replace value with values readed from scriptable objects later
            _animator = GetComponent<Animator>();
            _destination = transform.position;
        }

        private void Start()
        {
            _gridPosition = GridLevel.Instance.GetGridPosition(transform.position);
            GridLevel.Instance.AddUnitAtGridPosition(_gridPosition, this);
        }
        private void Update()
        {
            _distanceToDestination = Vector3.Distance(transform.position, _destination);
            if (_distanceToDestination >= _stoppingDistance)
            {
                var direction = (_destination - transform.position).normalized;
                transform.forward = Vector3.Lerp(transform.forward, direction, Time.deltaTime * 10f);
                transform.position += direction * _movementSpeed * Time.deltaTime;
                _animator.SetBool("isMoving", true);
            }
            else
            {
                _animator.SetBool("isMoving", false);
            }

            GridPosition newPos = GridLevel.Instance.GetGridPosition(transform.position);
            if (newPos != _gridPosition)
            {
                GridLevel.Instance.UpdateUnitAtGridPosition(this, _gridPosition, newPos);
                _gridPosition = newPos;
            }
        }

        internal void SetDestination()
        {
            _destination = InputManager.GetMousePosition();
        }
        internal void SetDestination(Vector3 destination)
        {
            _destination = destination;
        }
    }
}