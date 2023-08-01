using UnityEngine;
using FishNet;
using FishNet.Object;
using LuminaStudio.Core.Cameras;

namespace LuminaStudio.Core.InputSystem
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;
        private static Camera _mainCamera;
        private static Ray _raycast;
        private static RaycastHit _hit;
        private static Vector3 _mousePosition;
        private static Transform indicator;
        [SerializeField]
        private LayerMask _layerMask;

        void Awake()
        {
            Instance = this;
            _mainCamera = CameraManager.mainCamera;
            indicator = transform.GetChild(0);
        }

        void Update()
        {
            ShowIndicator();
        }

        private void ShowIndicator()
        {
            MouseClick(_layerMask);
            indicator.position = _mousePosition;
        }

        public static void MouseClick(LayerMask mask)
        {
            _raycast = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_raycast, out _hit, float.MaxValue, mask))
            {
                _mousePosition = _hit.point;
            }
        }
        public static Ray GetRayCast()
        {
            return _raycast;
        }

        public static Vector3 GetMousePosition()
        {
            return _mousePosition;
        }

        public static bool IsClicked()
        {
            if (Input.GetMouseButtonDown(0))
            {
                return true;
            }
            return false;
        }
    }
}
