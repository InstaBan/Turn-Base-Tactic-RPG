using Cinemachine;
using LuminaStudio.Core.InputSystem;
using UnityEditor.PackageManager;
using UnityEngine;

namespace LuminaStudio.Core.Cameras
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] 
        private float _moveSpeed = 10f;
        [SerializeField]
        private float _rotationSpeed = 100f;
        [SerializeField]
        private float _zoomSpeed = 5f;
        [SerializeField] 
        private const float MIN_ZOOM_OFFSET_Y = 2f;
        [SerializeField]
        private const float MAX_ZOOM_OFFSET_Y = 12f;
        private CinemachineVirtualCamera _virtualCamera;
        private CinemachineTransposer _transposer;
        private Vector3 _zoomOffset;

        private void Start()
        {
            _virtualCamera = CameraManager.GetVirtualCamera();
            _transposer = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            _zoomOffset = _transposer.m_FollowOffset;
        }
        private void Update()
        {
            InputManager.UpdatePositionByAxis(transform, _moveSpeed);
            InputManager.UpdateRotationByAxis(transform, _rotationSpeed);
            InputManager.UpdateOffsetMouseWheel(ref _transposer.m_FollowOffset, 
                                          ref _zoomOffset, _zoomSpeed, 
                                                MIN_ZOOM_OFFSET_Y, MAX_ZOOM_OFFSET_Y);
        }
    }
}
