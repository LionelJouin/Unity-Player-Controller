using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        public float Speed;
        public float DashSpeed;
        public int DashTime;
        public float AnimationTolerance = 0.1f;

        public bool IsMoving = false;

        private CharacterController _characterController;
        private int _currentDashTime;
        private float _defaultPositionY;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _currentDashTime = DashTime;
            _defaultPositionY = transform.position.y + _characterController.skinWidth;
        }

        private void FixedUpdate()
        {
            var axisHorizontal = Input.GetAxis("Horizontal");
            var axisVertical = Input.GetAxis("Vertical");
            var moveDirection = new Vector3(axisHorizontal, 0, axisVertical);
            var speed = Speed;

            IsMoving = axisVertical > AnimationTolerance || Math.Abs(axisHorizontal) > AnimationTolerance;

            if (!_characterController.isGrounded)
                transform.position = new Vector3(
                    transform.position.x,
                    _defaultPositionY,
                    transform.position.z);

            if (moveDirection.sqrMagnitude > 0.1f)
                transform.rotation = Quaternion.LookRotation(moveDirection);

            if (Input.GetButtonDown("Dash") && _currentDashTime == DashTime)
            {
                _currentDashTime = 0;
                Debug.Log("Dash");
            }

            if (_currentDashTime < DashTime)
            {
                speed = DashSpeed;
                _currentDashTime++;
            }

            _characterController.Move(moveDirection * Time.deltaTime * speed);
        }

    }
}
