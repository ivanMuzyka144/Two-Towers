using UnityEngine;

namespace CodeBase.Services.InputServiceLogic
{
  public interface IInputService : IService
  {
    Vector2 KeyboardAxis { get; }
    Vector2 MouseAxis { get; }
    bool JumpButtonPressed { get; }
    bool LeftMousePressed { get; }
    bool RightMousePressed { get; }
    bool LeftShiftPressed { get; }
    void RecordInput();
  }
}