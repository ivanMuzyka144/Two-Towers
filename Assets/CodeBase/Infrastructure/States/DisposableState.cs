using System;
using CodeBase.Services;
using IDisposable = System.IDisposable;

namespace CodeBase.Infrastructure.States
{
  public class DisposableState : IState
  {
    private readonly GameStateMachine _stateMachine;
    private AllServices _services;
    private readonly SceneLoader _sceneLoader;

    public DisposableState(GameStateMachine stateMachine, AllServices services, SceneLoader sceneLoader)
    {
      _stateMachine = stateMachine;
      _services = services;
      _sceneLoader = sceneLoader;
    }
    public void Enter()
    {
      DisposeServices();
      _sceneLoader.LoadSameScene(() => _stateMachine.Enter<LoadLevelState,string>("Main"));
    }

    public void Exit()
    {
      
    }
    
    private void DisposeServices()
    {
      foreach (IService service in _services.Services)
      {
        if (service is IDisposable)
          ((IDisposable)service).Dispose();
      }
    }
  }
}