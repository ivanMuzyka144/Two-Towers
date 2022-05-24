
using UnityEngine;

namespace CodeBase.Logic.PlayerLogic
{
    [RequireComponent(typeof(CharacterController))]

    public class CameraRotator : MonoBehaviour
    {
        [Space(10)]
        [SerializeField] private float _lookSpeed = 2.0f;
        [SerializeField] private float _lookXLimit = 45.0f;

        private float _rotationX = 0;

        private Camera _playerCamera;
        private bool _setuped;

        public void Construct(Camera cam)
        {
            _playerCamera = cam;// TODO: add player speed from static data,, Input service
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
            _rotationX += -Input.GetAxis("Mouse Y") * _lookSpeed;
            _rotationX = Mathf.Clamp(_rotationX, -_lookXLimit, _lookXLimit);
            _playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * _lookSpeed, 0);
        }
    }
}