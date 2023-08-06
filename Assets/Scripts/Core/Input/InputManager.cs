using Cinemachine;
using LuminaStudio.Core.Cameras;
using LuminaStudio.Core.Events;
using UnityEngine;

namespace LuminaStudio.Core.Input
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;
        private static Camera _mainCamera;
        private static CinemachineVirtualCamera _virtualCamera;
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
            _mainCamera = CameraManager.GetMainCamera();
            _virtualCamera = CameraManager.GetVirtualCamera();

        }

        private void Update()
        {
            ShowIndicator();
        }

        #region Visual
        private void ShowIndicator()
        {
            MousePosition(_layerMask);
            indicator.position = _mousePosition;
        }
        #endregion

        public static void MousePosition(LayerMask mask)
        {
            _raycast = _mainCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);
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

        public static bool IsMouseLeftClicked()
        {
            return UnityEngine.Input.GetMouseButtonDown(0);
        }
        public static void UpdatePositionByAxis(Transform trans, float speed)
        {
            var horizontalInput = UnityEngine.Input.GetAxis("Horizontal");
            var verticalInput = UnityEngine.Input.GetAxis("Vertical");
            if (Mathf.Approximately(horizontalInput, 0f) && Mathf.Approximately(verticalInput, 0f))
            {
                return;
            }
            var moveDirection = new Vector3(horizontalInput, 0f, verticalInput);
            var moveDir = trans.TransformDirection(moveDirection);
            trans.position += moveDir * speed * Time.deltaTime;
        }
        public static void UpdateRotationByAxis(Transform trans, float speed)
        {
            if (!UnityEngine.Input.GetMouseButton(1)) return;
            var mouseX = UnityEngine.Input.GetAxis("Mouse X");
            var rotationAmount = mouseX * speed * Time.deltaTime;
            var currentRotation = trans.eulerAngles;
            trans.eulerAngles = new Vector3(currentRotation.x, currentRotation.y + rotationAmount, currentRotation.z);
        }
        public static void UpdateOffsetMouseWheel(ref Vector3 offset, ref Vector3 newSet, float zoomSpeed, float min, float max)
        {
            switch (UnityEngine.Input.mouseScrollDelta.y)
            {
                case > 0:
                    newSet.y -= 1f;
                    break;
                case < 0:
                    newSet.y += 1f;
                    break;
            }

            newSet.y = Mathf.Clamp(newSet.y, min, max);
            offset = Vector3.Lerp(offset, newSet, Time.deltaTime * zoomSpeed);
        }
    }
}
