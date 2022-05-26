using CodeBase.Services.InputServiceLogic;
using UnityEngine;

namespace CodeBase.Logic.PlayerLogic
{
  [RequireComponent(typeof(CharacterController))]
  public class PlayerMover : MonoBehaviour
  {
    [SerializeField] private CharacterController characterController;

    private IInputService _inputService;
    
    private float _walkingSpeed = 7.5f;
    private float _runningSpeed = 11.5f;
    private float _jumpSpeed = 8.0f;
    private float _gravity = 20.0f;

    private Vector3 _moveDirection;

    private bool _setuped;


    public void Construct(IInputService inputService)
    {
      _inputService = inputService;
      _setuped = true;
    }

    void Update()
    {
      if (_setuped)
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
      Vector2 keyboardAxis = _inputService.KeyboardAxis;
      float curSpeedX = GetSpeed() * keyboardAxis.y;
      float curSpeedY = GetSpeed() * keyboardAxis.x;
      _moveDirection = (transform.forward * curSpeedX) + (transform.right * curSpeedY);
    }

    private float GetSpeed() =>
      _inputService.LeftShiftPressed ? _runningSpeed : _walkingSpeed;

    private void PerformJump(float movementDirectionY) =>
      _moveDirection.y = CanJump() ? _jumpSpeed : movementDirectionY;

    private bool CanJump() =>
      _inputService.JumpButtonPressed && characterController.isGrounded;

    private void ApplyGravity()
    {
      if (!characterController.isGrounded)
        _moveDirection.y -= _gravity * Time.deltaTime;
    }
  }
}