using System;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using IDisposable = System.IDisposable;

namespace CodeBase.Infrastructure.States
{
  public class DisposableState : IState
  {
    private readonly GameStateMachine _stateMachine;
    private AllServices _services;
    private readonly SceneLoader _sceneLoader;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadService;

    public DisposableState(GameStateMachine stateMachine, 
      AllServices services, 
      SceneLoader sceneLoader,
      ISaveLoadService saveLoadService)
    {
      _stateMachine = stateMachine;
      _services = services;
      _sceneLoader = sceneLoader;
      _saveLoadService = saveLoadService;
    }
    public void Enter()
    {
      SavedProgress();
      DisposeServices();
      _sceneLoader.LoadSameScene(() => _stateMachine.Enter<LoadLevelState,string>("Main"));
    }

    private void SavedProgress() => 
      _saveLoadService.SaveProgress();

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