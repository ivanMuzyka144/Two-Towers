using System.Collections;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services.CursorService;
using CodeBase.UI;
using CodeBase.UI.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class GameLoopState : IState
  {
    private const float EndGameDelay = 0.6f;
    
    private readonly GameStateMachine _stateMachine;
    private readonly ICursorService _cursorService;
    private readonly IGameFactory _factory;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly IWindowService _windowService;

    public GameLoopState(GameStateMachine stateMachine, 
      ICursorService cursorService, 
      IGameFactory factory,
      ICoroutineRunner coroutineRunner,
      IWindowService windowService)
    {
      _stateMachine = stateMachine;
      _cursorService = cursorService;
      _factory = factory;
      _coroutineRunner = coroutineRunner;
      _windowService = windowService;
    }

    public void Enter()
    {
      Time.timeScale = 1;
      _cursorService.HideCursor();
      _factory.OnAimLevelCompleted += HandleLevelCompleted;
    }

    public void Exit()
    {
      _factory.OnAimLevelCompleted -= HandleLevelCompleted;
    }

    private void HandleLevelCompleted() => 
      _coroutineRunner.StartCoroutine(CO_ShowEndGamePanel(EndGameDelay));

    private IEnumerator CO_ShowEndGamePanel(float delay)
    {
      yield return new WaitForSeconds(delay);
      Time.timeScale = 0;
      _cursorService.ShowCursor();
      
      LevelCompletedWindow levelCompletedWindow = _windowService.Open(WindowId.LevelCompleted) as LevelCompletedWindow;
      levelCompletedWindow.Construct(Restart);
    }

    private void Restart()
    {
      _stateMachine.Enter<DisposableState>();
    }
  }
}