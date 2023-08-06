using LuminaStudio.Unit;
using UnityEngine;

namespace LuminaStudio.Grid
{
    public class GridTest : MonoBehaviour
    {
        [SerializeField]
        private UnitBase _unit;
        private void Start()
        {
            
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                _unit.GetUnitMovement().GetValidGridPositions();
            }
        }
    }
}
