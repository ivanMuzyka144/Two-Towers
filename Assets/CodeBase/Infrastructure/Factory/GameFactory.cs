using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic.PlayerLogic;
using CodeBase.Logic.Tower;
using CodeBase.Logic.Tower.ElevatorLogic;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SharedData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
    public GameObject Hud => _hud;
    public Tower Tower => _tower;
    public Player Hero => _hero;

    private readonly IAssetProvider _assets;
    private readonly IPersistentProgressService _persistentProgressService;
    private readonly ISharedDataService _sharedDataService;
    
    private GameObject _hud;
    private Tower _tower;
    private Player _hero;
    
    public GameFactory(IAssetProvider assets, IPersistentProgressService persistentProgressService, ISharedDataService sharedDataService)
    {
      _assets = assets;
      _persistentProgressService = persistentProgressService;
      _sharedDataService = sharedDataService;
    }

    public void Register(ISavedProgressReader progressReader)
    {
      if (progressReader is ISavedProgress progressWriter)
        ProgressWriters.Add(progressWriter);

      ProgressReaders.Add(progressReader);
    }

    public void Cleanup()
    {
      ProgressReaders.Clear();
      ProgressWriters.Clear();
    }

    public Player CreateHero(Vector3 at, Quaternion rotation)
    {
      _hero = _assets.Instantiate(AssetPath.HeroPath, at, rotation).GetComponent<Player>();
      _hero.Construct(Camera.main); //TODO rework
      return _hero;
    }

    public GameObject CreateHud()
    {
      _hud = _assets.Instantiate(AssetPath.HudPath);
      return _hud;
    }

    public Tower CreateTower(Vector3 at)
    {
      _tower = _assets.Instantiate(AssetPath.TowerPath, at).GetComponent<Tower>();
      _tower.Construct(_sharedDataService);
      return _tower;
    }

    public Room CreateRoom(Transform parent)=> 
      _assets.Instantiate(AssetPath.RoomPath, parent).GetComponent<Room>();

    public Elevator CreateElevator(Vector3 at)
    {
      Elevator elevator = _assets.Instantiate(AssetPath.ElevatorPath, at).GetComponent<Elevator>();
      elevator.Construct(_tower, _hero, _sharedDataService.SharedData.ElevatorData);
      return elevator;
    }

    private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
    {
      GameObject gameObject = _assets.Instantiate(path: prefabPath, at: at);
      RegisterProgressWatchers(gameObject);

      return gameObject;
    }

    private GameObject InstantiateRegistered(string prefabPath)
    {
      GameObject gameObject = _assets.Instantiate(path: prefabPath);
      RegisterProgressWatchers(gameObject);

      return gameObject;
    }

    private void RegisterProgressWatchers(GameObject gameObject)
    {
      foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
        Register(progressReader);
    }
  }
}