using FishNet.Object;
using UnityEngine;

namespace LuminaStudio.Player
{
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField]
        private float _mMovementSpeed = 5f;
        [SerializeField]
        private float _mRotationSpeed = 180f;
        //[SerializeField]
        //private float _mCameraFollowDistanceX = 0f;
        //[SerializeField]
        //private float _mCameraFollowDistanceY = 0f;
        //[SerializeField]
        //private float _mCameraFollowDistanceZ = -3f;
        private Camera _mPlayerCamera;
        private CharacterController _characterController;
        public override void OnStartClient()
        {
            base.OnStartClient();
            if (base.IsOwner) 
            {
                _mPlayerCamera = Camera.main;
                _mPlayerCamera.transform.SetParent(transform);

                _mPlayerCamera.transform.localPosition = new Vector3(0f, 2f, -5f);
                _mPlayerCamera.transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
                Vector3 lookDirection = transform.forward;
                _mPlayerCamera.transform.LookAt(transform.position + lookDirection);
            }
            else 
            {
                gameObject.GetComponent<PlayerController>().enabled = false;
            }
        }
        void Start()
        {
        
        }

        private void Update()
        {
            Movement();
        }

        private void Movement()
        {
            // translate
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * _mMovementSpeed * Time.deltaTime;
            transform.Translate(movement, Space.Self);

            // rotate
            float mouseX = Input.GetAxis("Mouse X");
            float rotationAmount = mouseX * _mRotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, rotationAmount);
        }
    }
}
