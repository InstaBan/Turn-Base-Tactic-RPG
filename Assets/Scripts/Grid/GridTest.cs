using LuminaStudio.Core.InputSystem;
using UnityEngine;

namespace LuminaStudio.Grid
{
    public class GridTest : MonoBehaviour
    {
        [SerializeField] private Transform _gridDebugObjecTransform;
        private GridBase _gridBase;
        private void Start()
        {
            _gridBase = new GridBase(10, 10, 2f);
            _gridBase.DebugObjects(_gridDebugObjecTransform);
        }
        void Update()
        {
            Debug.Log(_gridBase.GetGridPosition((InputManager.GetMousePosition())));
        }
    }
}
