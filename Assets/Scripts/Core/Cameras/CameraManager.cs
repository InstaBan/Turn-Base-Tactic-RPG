using Cinemachine;
using UnityEngine;

namespace LuminaStudio.Core.Cameras
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance { get; private set; }
        [SerializeField]
        private static Camera _mainCamera;
        [SerializeField]
        private static CinemachineVirtualCamera _virtualCamera;


        public void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Error: Duplicate CameraManager Found in: " + transform + " / " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
            _mainCamera = transform.GetComponentInChildren<Camera>();
            _virtualCamera = transform.GetComponentInChildren<CinemachineVirtualCamera>();
        }

        public static Camera GetMainCamera()
        {
            return _mainCamera;
        }
        public static CinemachineVirtualCamera GetVirtualCamera()
        {
            return _virtualCamera;
        }
    }
}