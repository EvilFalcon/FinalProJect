using CodeBase.Data;
using CodeBase.Infrastructure.Service.PersistentProgress;
using CodeBase.Services.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Hero
{
    public class HeroMove : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _movementSpeed;

        private IInputService _inputService;
        private Camera _camera;
        private bool _isActiv;

        private void Start() =>
            _camera = Camera.main;

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;

            _characterController.Move(movementVector * (_movementSpeed * Time.deltaTime));
        }

        public void UpdateProgress(PlayerProgress playerProgress) =>
            playerProgress.WorldData.PlayerPositionOnLevel = new PlayerPositionOnLevel(CerentLevel(), transform.position.AsVectorData());

        public void LoadProgres(PlayerProgress playerProgress)
        {
            if (CerentLevel() == playerProgress.WorldData.PlayerPositionOnLevel.Lavel)
            {
                Vector3Data savedPosition = playerProgress.WorldData.PlayerPositionOnLevel.PlayerPosition;

                if (savedPosition == default)
                    return;

                Warp(savedPosition);
            }
        }

        private void Warp(Vector3Data savedPosition)
        {
            _characterController.enabled = false;
            transform.position = savedPosition.AnUnityVector().Addy(_characterController.height);
            _characterController.enabled = true;
        }

        public void Construct(IInputService inputService) =>
            _inputService = inputService;

        private string CerentLevel() =>
            SceneManager.GetActiveScene().name;
    }
}