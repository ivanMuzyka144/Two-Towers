
using CodeBase.Services.InputServiceLogic;
using UnityEngine;

namespace CodeBase.Logic.PlayerLogic
{
    [RequireComponent(typeof(CharacterController))]

    public class CameraRotator : MonoBehaviour
    {
        [Space(10)]
        [SerializeField] private float _lookSpeed = 2.0f;
        [SerializeField] private float _lookXLimit = 45.0f;

        private IInputService _inputService;
        private Camera _playerCamera;
        private bool _setuped;

        private float _rotationX = 0;

        public void Construct(Camera cam, IInputService inputService)
        {
            _inputService = inputService;
            _playerCamera = cam;
            _setuped = true;
        }

        void Update()
        {
            if (_setuped)
            {
                RotatePerson();
            }
        }

        private void RotatePerson()
        {
            Vector2 mouseAxis = _inputService.MouseAxis;
            _rotationX += -mouseAxis.y * _lookSpeed;
            _rotationX = Mathf.Clamp(_rotationX, -_lookXLimit, _lookXLimit);
            _playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, mouseAxis.x * _lookSpeed, 0);
        }
    }
}