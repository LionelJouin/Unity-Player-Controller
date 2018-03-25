using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {

        private PlayerController _playerController;
        private PlayerRaycaster _playerRaycaster;
        private Animator _animator;
        private ParticleSystem _particleSystem;

        void Start()
        {
            _playerController = GetComponent<PlayerController>();
            _playerRaycaster = GetComponent<PlayerRaycaster>();
            _animator = GetComponentInChildren<Animator>();
            _particleSystem = GetComponent<ParticleSystem>();
        }

        void Update()
        {
            var emission = _particleSystem.emission;
            emission.enabled = _playerController.IsMoving;

            _animator.SetBool("Moving", _playerController.IsMoving);
            _animator.SetBool("HasObject", _playerRaycaster.HasObject);
        }
    }
}
