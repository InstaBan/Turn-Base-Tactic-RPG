using UnityEngine;
using LuminaStudio.Calculation.Logic;
using LuminaStudio.Core.Input;
using LuminaStudio.Grid;
using LuminaStudio.Unit.Actions;

namespace LuminaStudio.Unit
{
    public class Unit : MonoBehaviour
    {
        #region attributes
        [SerializeField]
        private GridPosition _gridPosition;
        #endregion

        #region Actions
        private MovementAction _movementAction;
        private TestAction _testAction;
        #endregion

        private void Awake() // replace with OnstartClient when networked
        {
            //base.OnStartClient();
            // replace value with values readed from scriptable objects later
            _movementAction = GetComponent<MovementAction>();
            _testAction = GetComponent<TestAction>();
        }

        private void Start()
        {
            _gridPosition = GridLevel.Instance.GetGridPosition(transform.position);
            GridLevel.Instance.AddUnitAtGridPosition(_gridPosition, this);
        }
        private void Update()
        {

            GridPosition newPos = GridLevel.Instance.GetGridPosition(transform.position);
            if (newPos != _gridPosition)
            {
                GridLevel.Instance.UpdateUnitAtGridPosition(this, _gridPosition, newPos);
                _gridPosition = newPos;
            }
        }

        public MovementAction GetUnitMovement()
        {
            return _movementAction;
        }

        public TestAction GeTestAction()
        {
            return _testAction;
        }

        public GridPosition GetGridPosition()
        {
            return _gridPosition;
        }
    }
}