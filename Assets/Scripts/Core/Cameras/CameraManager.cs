using UnityEngine;

namespace LuminaStudio.Core.Cameras
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance { get; private set; }
        public static Camera mainCamera;

        public void Awake()
        {
            Instance = this;
            mainCamera = Camera.main;
        }
    }
}