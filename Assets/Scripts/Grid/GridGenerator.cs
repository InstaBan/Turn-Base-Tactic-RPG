using LuminaStudio.Core.InputSystem;
using UnityEngine;

namespace LuminaStudio.Grid
{
    public class GridGenerator : MonoBehaviour
    {
        private GridManager _gridManager;
        private void Start()
        {
            _gridManager = new GridManager(10, 10, 2f);
        }
        void Update()
        {
            Debug.Log(_gridManager.GetGridPosition((InputManager.GetMousePosition())));
        }
    }
}
