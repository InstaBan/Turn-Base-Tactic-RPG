using Cinemachine;
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
            MouseClick(_layerMask);
            indicator.position = _mousePosition;
        }
        #endregion

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
        public static void UpdatePositionByAxis(Transform trans, float speed)
        {
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");
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
            if (!Input.GetMouseButton(1)) return;
            var mouseX = Input.GetAxis("Mouse X");
            var rotationAmount = mouseX * speed * Time.deltaTime;
            var currentRotation = trans.eulerAngles;
            trans.eulerAngles = new Vector3(currentRotation.x, currentRotation.y + rotationAmount, currentRotation.z);
        }
        public static void UpdateOffsetMouseWheel(ref Vector3 offset, ref Vector3 newSet, float zoomSpeed, float min, float max)
        {
            if (Input.mouseScrollDelta.y < 0)
            {
                newSet.y -= 1f;
            }

            if (Input.mouseScrollDelta.y > 0)
            {
                newSet.y += 1f;
            }
            newSet.y = Mathf.Clamp(newSet.y, min, max);
            offset = Vector3.Lerp(offset, newSet, Time.deltaTime * zoomSpeed);
        }
    }
}
