using UnityEngine;
using FishNet;
using FishNet.Object;
using LuminaStudio.Core.Cameras;

namespace LuminaStudio.Core.InputSystem
{
    public class InputManager : MonoBehaviour
    {
        private static InputManager instance;
        private CameraManager _cameraManager;
        private Camera _mainCamera;
        private Ray _raycast;
        private RaycastHit _hit;
        private static Vector3 _mousePosition;
        private Transform indicator;

        void Awake()
        {
            _cameraManager = FindAnyObjectByType<CameraManager>();
            _mainCamera = _cameraManager.mainCamera;
            indicator = transform.GetChild(0);
        }

        void Update()
        {
            RaycastByMousePosition();
        }

        private void RaycastByMousePosition()
        {
            _raycast = _mainCamera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(_raycast, out _hit, float.MaxValue, LayerMask.GetMask("Ground"));
            _mousePosition = _hit.point;
            indicator.position = _mousePosition;
        }
        public Ray GetRaycastPointRay()
        {
            return _raycast;
        }

        public static Vector3 GetMousePosition()
        {
            return _mousePosition;
        }
    }
}
