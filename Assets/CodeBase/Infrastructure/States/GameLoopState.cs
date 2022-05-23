using CodeBase.Services.CursorService;

namespace CodeBase.Infrastructure.States
{
  public class GameLoopState : IState
  {
    private readonly GameStateMachine _stateMachine;
    private readonly ICursorService _cursorService;

    public GameLoopState(GameStateMachine stateMachine, ICursorService cursorService)
    {
      _stateMachine = stateMachine;
      _cursorService = cursorService;
    }

    public void Enter()
    {
      _cursorService.HideCursor();
    }

    public void Exit()
    {
    }
  }
}