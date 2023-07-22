using System.Collections;
using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Object;
using Unity.VisualScripting;
using UnityEngine;

namespace LuminaStudio.Entity
{
    public class EntityMovement : MonoBehaviour
    {
        #region attributes
        private Vector3 _destination;
        private float _stoppingDistance;
        private float _distanceToDestination;
        private float _movementSpeed;
        #endregion

        private void Awake() // replace with OnstartClient when networked
        {
            //base.OnStartClient();

            // replace value with values readed from scriptable objects later
            _movementSpeed = 5f;
            _stoppingDistance = 0.1f;
        }
        private void Update()
        {
            _distanceToDestination = Vector3.Distance(transform.position, _destination);
            if (_distanceToDestination >= _stoppingDistance)
            {
                var direction = (_destination - transform.position).normalized;
                transform.position += direction * _movementSpeed * Time.deltaTime;
            }
            // replace with event maybe?
            if (Input.GetMouseButtonDown(0))
            {
                Move(new Vector3(3, 0, 3));
            }
        }

        private void Move(Vector3 destination)
        {
            _destination = destination;
        }
    }
}