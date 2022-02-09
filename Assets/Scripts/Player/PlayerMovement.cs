using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Joystick _joystick;

        private PlayerContainer _player;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _player = GetComponent<PlayerContainer>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Vector3 direction = Vector3.forward * _joystick.Vertical + Vector3.right * _joystick.Horizontal;
            _rigidbody.AddForce(direction * _speed * Time.fixedDeltaTime, ForceMode.VelocityChange);

            bool isMoving = direction.sqrMagnitude > 0;
            if (isMoving)
            {
                transform.localRotation = Quaternion.Euler(0, direction.Angle(), 0);
            }

            _player.PlayerAnimation.SetMoving(isMoving);
        }
    }
}
