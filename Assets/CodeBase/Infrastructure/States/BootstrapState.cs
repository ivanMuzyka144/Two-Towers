using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services;
using CodeBase.Services.CursorService;
using CodeBase.Services.InputServiceLogic;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.SharedData;
using CodeBase.Services.StaticData;
using CodeBase.UI.Factory;
using CodeBase.UI.Services;

namespace CodeBase.Infrastructure.States
{
  public class BootstrapState : IState
  {
    private const string Initial = "Initial";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _services = services;
      RegisterServices();
    }

    public void Enter() =>
      _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);

    public void Exit()
    {
    }

    private void RegisterServices()
    {
      _services.RegisterSingle(SharedDataService());
      _services.RegisterSingle<IInputService>(new InputService());
      _services.RegisterSingle<ICursorService>(new GameCursorService());
      _services.RegisterSingle(StaticDataService());
      _services.RegisterSingle<IAssetProvider>(new AssetProvider());
      _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
      _services.RegisterSingle<IUiFactory>(new UiFactory(_services.Single<IAssetProvider>(), _services.Single<IStaticDataService>()) );
      _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUiFactory>()));
      _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>(),
        _services.Single<IPersistentProgressService>(), 
        _services.Single<ISharedDataService>(), 
        _services.Single<IStaticDataService>(),
        _services.Single<IInputService>()));
      _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
    }


    private void EnterLoadLevel() =>
      _stateMachine.Enter<LoadProgressState>();

    private static ISharedDataService SharedDataService()
    {
      ISharedDataService sharedDataService = new SharedDataService();
      sharedDataService.SharedData = new GameSharedData();
      return sharedDataService;
    }

    private static IStaticDataService StaticDataService()
    {
      IStaticDataService staticDataService = new StaticDataService();
      staticDataService.Load();
      return staticDataService;
    }
  }
}