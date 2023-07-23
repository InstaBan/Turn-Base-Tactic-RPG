using UnityEngine;
using LuminaStudio.Core.Cameras;

namespace LuminaStudio.Core.InputSystem
{
    public class InputManager : MonoBehaviour
    {
        public CameraManager cameraManager;
        public Camera mainCamera;
        public Ray raycast;
        public RaycastHit hit;
        public Transform indicator;

        void Awake()
        {
            cameraManager = FindAnyObjectByType<CameraManager>();
            mainCamera = cameraManager.mainCamera;
            indicator = transform.GetChild(0);
        }

        void Update()
        {
            RaycastByMousePosition();
        }

        private void RaycastByMousePosition()
        {
            raycast = mainCamera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(raycast, out hit, float.MaxValue, LayerMask.GetMask("Ground"));
            indicator.position = hit.point;
        }
        ///<summary>
        ///     returns the current raycast point by mouse position
        ///</summary>
        public Ray GetRaycastPointMouse()
        {
            return raycast;
        }
    }
}
