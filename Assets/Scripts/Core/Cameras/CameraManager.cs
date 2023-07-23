using UnityEngine;

namespace LuminaStudio.Core.Cameras
{
    public class CameraManager : MonoBehaviour
    {
        public Camera mainCamera;

        public void Awake()
        {
            mainCamera = Camera.main;
        }
    }
}