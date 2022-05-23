
using UnityEngine;

namespace CodeBase.Logic.PlayerLogic
{
    [RequireComponent(typeof(CharacterController))]

    public class PlayerMover : MonoBehaviour
    {
        private const string VerticalAxisName = "Vertical";
        private const string HorizontalAxisName = "Horizontal";
        [SerializeField] private CharacterController _characterController;
    
        public float walkingSpeed = 7.5f;
        public float runningSpeed = 11.5f;
        public float jumpSpeed = 8.0f;
        public float gravity = 20.0f;
        public float lookSpeed = 2.0f;
        public float lookXLimit = 45.0f;

        float rotationX = 0;

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

            float curSpeedX = (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis(VerticalAxisName);
            float curSpeedY = (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis(HorizontalAxisName);
            return (forward * curSpeedX) + (right * curSpeedY);
        }

        private Vector3 ProcessJumping(Vector3 moveDirection)
        {
            float movementDirectionY = moveDirection.y;
        
            if (Input.GetButton("Jump") && _characterController.isGrounded)
            {
                moveDirection.y = jumpSpeed;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            if (!_characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }
            return moveDirection;
        }

        private void RotatePerson()
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            _playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}