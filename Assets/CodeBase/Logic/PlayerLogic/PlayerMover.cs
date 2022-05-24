using UnityEngine;

namespace CodeBase.Logic.PlayerLogic
{
    [RequireComponent(typeof(CharacterController))]

    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
    
        private float _walkingSpeed = 7.5f;
        private float _runningSpeed = 11.5f;
        private float _jumpSpeed = 8.0f;
        private float _gravity = 20.0f;

        private Vector3 _moveDirection;

        private bool _setuped;
        
        public void Construct()
        {
            _setuped = true;
        }
        void Update()
        {
            if(_setuped)
                MovePlayer();
        }

        private void MovePlayer()
        {
            Vector3 prevMoveVector = _moveDirection;
            SetupMovementDirection();
            PerformJump(prevMoveVector.y);
            ApplyGravity();
            characterController.Move(_moveDirection * Time.deltaTime);
        }

        private void SetupMovementDirection()
        {
            float curSpeedX = GetSpeed() * Input.GetAxis("Vertical");
            float curSpeedY = GetSpeed() * Input.GetAxis("Horizontal");
            _moveDirection = (transform.forward * curSpeedX) + (transform.right * curSpeedY);
        }

        private float GetSpeed() => 
            Input.GetKey(KeyCode.LeftShift) ? _runningSpeed : _walkingSpeed;

        private void PerformJump(float movementDirectionY) => 
            _moveDirection.y = CanJump() ? _jumpSpeed : movementDirectionY;

        private bool CanJump() => 
            Input.GetButton("Jump") && characterController.isGrounded;

        private void ApplyGravity()
        {
            if (!characterController.isGrounded) 
                _moveDirection.y -= _gravity * Time.deltaTime;
        }
    }
}