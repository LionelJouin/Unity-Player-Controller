using UnityEngine;

namespace Assets.Scripts.Player {
    public class PlayerController : MonoBehaviour {
        private const float ROTATION_SPEED = 20f;

        public float Speed;
        public float DashSpeed;
        public int DashTime;

        private CharacterController _characterController;
        private int _currentDashTime;
        private float _defaultPositionY;


        private void Start() {
            _characterController = GetComponent<CharacterController>();
            _currentDashTime = DashTime;
            _defaultPositionY = transform.position.y + _characterController.skinWidth;
        }

        private void FixedUpdate() {
            var moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            var speed = Speed;

            if (!_characterController.isGrounded)
                transform.position = new Vector3(
                    transform.position.x,
                    _defaultPositionY,
                    transform.position.z);

            if (moveDirection.sqrMagnitude > 0.05f)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * ROTATION_SPEED);

            if (Input.GetButtonDown("Dash") && _currentDashTime == DashTime) {
                _currentDashTime = 0;
                Debug.Log("Dash");
            }

            if (_currentDashTime < DashTime) {
                speed = DashSpeed;
                _currentDashTime++;
            }

            _characterController.Move(moveDirection * Time.deltaTime * speed);

        }

        private void Rotate(Vector3 destination) {

        }
    }
}
