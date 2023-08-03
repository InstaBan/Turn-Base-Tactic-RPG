using LuminaStudio.Core.InputSystem;
using UnityEditor.PackageManager;
using UnityEngine;

namespace LuminaStudio.Core.Cameras
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] 
        private static float _moveSpeed = 10f;

        private static float _rotationSpeed = 100f;
        private void Update()
        {
            InputManager.UpdatePositionByAxis(transform, _moveSpeed);
            InputManager.UpdateRotationByAxis(transform, _rotationSpeed);
        }
    }
}
