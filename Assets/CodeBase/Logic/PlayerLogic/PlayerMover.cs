
using UnityEngine;

namespace CodeBase.Logic.PlayerLogic
{
    [RequireComponent(typeof(CharacterController))]

    public class PlayerMover : MonoBehaviour
    {
        private const string VerticalAxisName = "Vertical";
        private const string HorizontalAxisName = "Horizontal";
        
        [SerializeField] private CharacterController _characterController;
    
        [Space(10)]
        [Header("Mover params")]
        [SerializeField] private float _walkingSpeed = 7.5f;
        [SerializeField] private float _runningSpeed = 11.5f;
        [SerializeField] private float _jumpSpeed = 8.0f;
        [SerializeField] private float _gravity = 20.0f;
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

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked; // TODO: Cursor service
            Cursor.visible = false;
        }

        void Update()
        {
            if (_setuped)
            {
                Vector3 moveDirection = SetupMoveDirection();
                moveDirection = ProcessJumping(moveDirection);
                _characterController.Move(moveDirection * Time.deltaTime);
                RotatePerson();
            }
        }

        private Vector3 SetupMoveDirection()
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            bool isRunning = Input.GetKey(KeyCode.LeftShift);

            float curSpeedX = (isRunning ? _runningSpeed : _walkingSpeed) * Input.GetAxis(VerticalAxisName);
            float curSpeedY = (isRunning ? _runningSpeed : _walkingSpeed) * Input.GetAxis(HorizontalAxisName);
            return (forward * curSpeedX) + (right * curSpeedY);
        }

        private Vector3 ProcessJumping(Vector3 moveDirection)
        {
            float movementDirectionY = moveDirection.y;
        
            if (Input.GetButton("Jump") && _characterController.isGrounded)
            {
                moveDirection.y = _jumpSpeed;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            if (!_characterController.isGrounded)
            {
                moveDirection.y -= _gravity * Time.deltaTime;
            }
            return moveDirection;
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