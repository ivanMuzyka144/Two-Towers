using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic.PlayerLogic;
using CodeBase.Logic.Shoot;
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
    public FirstTower FirstTower => _firstTower;
    public SecondTower SecondTower => _secondTower;
    public Player Hero => _hero;

    private readonly IAssetProvider _assets;
    private readonly IPersistentProgressService _persistentProgressService;
    private readonly ISharedDataService _sharedDataService;
    
    private GameObject _hud;
    private Player _hero;
    private FirstTower _firstTower;
    private SecondTower _secondTower;

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

    public FirstTower CreateFirstTower(Vector3 at)
    {
      _firstTower = _assets.Instantiate(AssetPath.FirstTowerPath, at).GetComponent<FirstTower>();
      _firstTower.Construct(this, _sharedDataService);
      return _firstTower;
    }

    public SecondTower CreateSecondTower(Vector3 at)
    {
      _secondTower = _assets.Instantiate(AssetPath.SecondTowerPath, at).GetComponent<SecondTower>();
      _secondTower.Construct(_sharedDataService, this);
      return _secondTower;
    }

    public Room CreateFirstRoom(Transform parent)=> 
      _assets.Instantiate(AssetPath.FirstRoomPath, parent).GetComponent<Room>();

    public Room CreateSecondRoom(Transform parent)=> 
      _assets.Instantiate(AssetPath.SecondRoomPath, parent).GetComponent<Room>();

    public Elevator CreateElevator(Vector3 at)
    {
      Elevator elevator = _assets.Instantiate(AssetPath.ElevatorPath, at).GetComponent<Elevator>();
      elevator.Construct(_firstTower, _hero, _sharedDataService.SharedData.ElevatorData);
      return elevator;
    }

    public ObstacleCourse CreateObstacleCourse(int selectedFloor, float height)
    {
      Vector3 at = Vector3.Lerp(_firstTower.transform.position, _secondTower.transform.position, 0.5f);
      at.y = height;
      return _assets.Instantiate(AssetPath.ObstacleCoursePath, at).GetComponent<ObstacleCourse>();
    }

    public Bullet CreateBullet(Vector3 at, Quaternion rotation) => 
      _assets.Instantiate(AssetPath.BulletPath, at, rotation).GetComponent<Bullet>();

    public AimLevel CreateAimLevel(Vector3 at, Quaternion rotation)
    {
      AimLevel aimLevel = _assets.Instantiate(AssetPath.AimLevelPath, at, rotation).GetComponent<AimLevel>();
      aimLevel.Construct(this, _hero);
      return aimLevel;
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