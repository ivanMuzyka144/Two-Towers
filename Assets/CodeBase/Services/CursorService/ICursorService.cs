namespace CodeBase.Services.CursorService
{
  public interface ICursorService: IService
  {
    void HideCursor();
    void ShowCursor();
  }
}