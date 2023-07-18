using System.Collections;
using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    private float m_movementSpeed = 5f;
    [SerializeField]
    private float m_rotationSpeed = 180f;
    [SerializeField]
    private float m_cameraFollowDistanceX = 0f;
    [SerializeField]
    private float m_cameraFollowDistanceY = 0f;
    [SerializeField]
    private float m_cameraFollowDistanceZ = -3f;
    private Camera m_playerCamera;
    CharacterController characterController;
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner) 
        {
            m_playerCamera = Camera.main;
            m_playerCamera.transform.SetParent(transform);

            m_playerCamera.transform.localPosition = new Vector3(0f, 2f, -5f);
            m_playerCamera.transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
            Vector3 lookDirection = transform.forward;
            m_playerCamera.transform.LookAt(transform.position + lookDirection);
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
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * m_movementSpeed * Time.deltaTime;
        transform.Translate(movement, Space.Self);

        // rotate
        float mouseX = Input.GetAxis("Mouse X");
        float rotationAmount = mouseX * m_rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotationAmount);
    }
}
