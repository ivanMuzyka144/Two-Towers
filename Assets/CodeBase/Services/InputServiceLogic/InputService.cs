using UnityEngine;

namespace CodeBase.Services.InputServiceLogic
{
  public class InputService : IInputService
  {
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";
    private const string Jump = "Jump";

    public Vector2 KeyboardAxis { get; private set; }
    public Vector2 MouseAxis { get; private set; }
    
    public bool JumpButtonPressed { get; private set; }
    public bool LeftMousePressed { get; private set; }
    public bool RightMousePressed { get;  private set;}
    public bool LeftShiftPressed { get; private set; }

    public void RecordInput()
    {
      RecordKeyboard();
      RecordMouse();
      RecordJumpButton();
      RecordLeftShift();
    }

    private void RecordKeyboard() => 
      KeyboardAxis = new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));

    private void RecordMouse()
    {
      MouseAxis = new Vector2(Input.GetAxis(MouseX), Input.GetAxis(MouseY));
      LeftMousePressed = Input.GetKeyDown(KeyCode.Mouse0);
      RightMousePressed = Input.GetKeyDown(KeyCode.Mouse1);
    }

    private void RecordJumpButton() => 
      JumpButtonPressed = Input.GetButton(Jump);

    private void RecordLeftShift() =>
      LeftShiftPressed = Input.GetKey(KeyCode.LeftShift);
  }
}