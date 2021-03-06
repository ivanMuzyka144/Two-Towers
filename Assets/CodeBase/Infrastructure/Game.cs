using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using CodeBase.Services;

namespace CodeBase.Infrastructure
{
  public class Game
  {
    public GameStateMachine StateMachine;

    public Game(ICoroutineRunner coroutineRunner)
    {
      StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), AllServices.Container, coroutineRunner);
    }

    public void Update()
    {
      StateMachine.Update();
    }
  }
}