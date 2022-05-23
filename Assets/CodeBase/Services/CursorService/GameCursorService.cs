using UnityEngine;

namespace CodeBase.Services.CursorService
{
  public class GameCursorService : ICursorService
  {
    public void HideCursor()
    {
      Cursor.lockState = CursorLockMode.Locked; 
      Cursor.visible = false;
    }

    public void ShowCursor()
    {
      Cursor.lockState = CursorLockMode.None; 
      Cursor.visible = true;
    }
  }
}