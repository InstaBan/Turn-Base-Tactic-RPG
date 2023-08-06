using UnityEngine;
using LuminaStudio.Calculation.Logic;
using LuminaStudio.Core.Input;
using LuminaStudio.Grid;

namespace LuminaStudio.Unit
{
    public class UnitBase : MonoBehaviour
    {
        #region attributes
        [SerializeField]
        private GridPosition _gridPosition;

        private UnitMovement _unitMovement;
        #endregion

        private void Awake() // replace with OnstartClient when networked
        {
            //base.OnStartClient();
            // replace value with values readed from scriptable objects later
            _unitMovement = GetComponent<UnitMovement>();
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

        public UnitMovement GetUnitMovement()
        {
            return _unitMovement;
        }

        public GridPosition GetGridPosition()
        {
            return _gridPosition;
        }
    }
}