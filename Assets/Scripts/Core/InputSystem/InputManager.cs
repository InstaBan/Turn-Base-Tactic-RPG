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

        private void Awake()
        {
            Instance = this;
            indicator = transform.GetChild(0);
        }

        private void Start()
        {
            _mainCamera = CameraManager.mainCamera;
        }

        private void Update()
        {
            ShowIndicator();
        }

        #region Visual
        private void ShowIndicator()
        {
            MouseClick(_layerMask);
            indicator.position = _mousePosition;
        }
        #endregion

        #region Mouse
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
        #endregion

        public static void UpdatePositionByAxis(Transform trans, float speed)
        {
            var moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            var moveDir = trans.TransformDirection(moveDirection);
            trans.position += moveDir * speed * Time.deltaTime;
        }
        public static void UpdateRotationByAxis(Transform trans, float speed)
        {
            if (!Input.GetMouseButton(1)) return;
            var mouseX = Input.GetAxis("Mouse X");
            var rotationAmount = mouseX * speed * Time.deltaTime;
            var currentRotation = trans.eulerAngles;
            trans.eulerAngles = new Vector3(currentRotation.x, currentRotation.y + rotationAmount, currentRotation.z);
        }
    }
}
