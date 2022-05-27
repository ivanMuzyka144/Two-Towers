using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic.PlayerLogic;
using CodeBase.Logic.Shoot;
using CodeBase.Logic.Tower;
using CodeBase.Logic.Tower.ElevatorLogic;
using CodeBase.Services.InputServiceLogic;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SharedData;
using CodeBase.Services.StaticData;
using CodeBase.UI.Elements;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public class GameFactory : IGameFactory, IDisposable
  {
    public GameObject Hud => _hud;
    public FirstTower FirstTower => _firstTower;
    public SecondTower SecondTower => _secondTower;
    public Player Hero => _hero;
    public event Action OnAimLevelCompleted;

    private readonly IAssetProvider _assets;
    private readonly IPersistentProgressService _persistentProgressService;
    private readonly ISharedDataService _sharedDataService;
    private readonly IStaticDataService _staticDataService;
    private readonly IInputService _inputService;

    private GameObject _hud;
    private Player _hero;
    private FirstTower _firstTower;
    private SecondTower _secondTower;

    public GameFactory(IAssetProvider assets,
      IPersistentProgressService persistentProgressService,
      ISharedDataService sharedDataService,
      IStaticDataService staticDataService,
      IInputService inputService)
    {
      _assets = assets;
      _persistentProgressService = persistentProgressService;
      _sharedDataService = sharedDataService;
      _staticDataService = staticDataService;
      _inputService = inputService;
    }

    public void Cleanup()
    {
      _hud = null;
      _hero = null;
      _firstTower = null;
      _secondTower = null;
      OnAimLevelCompleted = null;
    }

    public Player CreateHero(Vector3 at, Quaternion rotation)
    {
      _hero = _assets.Instantiate(AssetPath.HeroPath, at, rotation).GetComponent<Player>();
      _hero.Construct(Camera.main, _sharedDataService, _inputService, _persistentProgressService);
      _sharedDataService.SharedData.PlayerData.SpawnPoint = at;
      return _hero;
    }

    public GameObject CreateHud()
    {
      _hud = _assets.Instantiate(AssetPath.HudPath);
      _hud.GetComponentInChildren<JumpPresenter>().Construct(_persistentProgressService);
      _hud.GetComponentInChildren<FirePresenter>().Construct(_persistentProgressService);
      _hud.GetComponentInChildren<AimPresenter>().Construct(_persistentProgressService);
      _hud.GetComponentInChildren<TimePresenter>().Construct(_persistentProgressService);
      return _hud;
    }

    public FirstTower CreateFirstTower(Vector3 at)
    {
      _firstTower = _assets.Instantiate(AssetPath.FirstTowerPath, at).GetComponent<FirstTower>();
      _firstTower.Construct(this, _sharedDataService, _staticDataService);
      return _firstTower;
    }

    public SecondTower CreateSecondTower(Vector3 at)
    {
      _secondTower = _assets.Instantiate(AssetPath.SecondTowerPath, at).GetComponent<SecondTower>();
      _secondTower.Construct(_sharedDataService, this, _staticDataService);
      return _secondTower;
    }

    public Room CreateFirstRoom(Transform parent) =>
      _assets.Instantiate(AssetPath.FirstRoomPath, parent).GetComponent<Room>();

    public Room CreateSecondRoom(Transform parent) =>
      _assets.Instantiate(AssetPath.SecondRoomPath, parent).GetComponent<Room>();

    public Elevator CreateElevator(Vector3 at)
    {
      Elevator elevator = _assets.Instantiate(AssetPath.ElevatorPath, at).GetComponent<Elevator>();
      elevator.Construct(_firstTower, _hero, _sharedDataService.SharedData.ElevatorData, _staticDataService);
      return elevator;
    }

    public ObstacleCourse CreateObstacleCourse(int selectedFloor, float height)
    {
      Vector3 at = Vector3.Lerp(_firstTower.transform.position, _secondTower.transform.position, 0.5f);
      at.y = height;
      ObstacleCourse prefab = _staticDataService.ForObstacle(selectedFloor);
      return _assets.Instantiate(prefab.gameObject, at).GetComponent<ObstacleCourse>();
    }

    public Bullet CreateBullet(Vector3 at, Quaternion rotation) =>
      _assets.Instantiate(AssetPath.BulletPath, at, rotation).GetComponent<Bullet>();

    public AimLevel CreateAimLevel(int selectedFloor, Vector3 at, Quaternion rotation)
    {
      AimLevel prefab = _staticDataService.ForAimLevel(selectedFloor);
      AimLevel aimLevel = _assets.Instantiate(prefab.gameObject, at, rotation).GetComponent<AimLevel>();
      aimLevel.Construct(this, _hero, _persistentProgressService);
      aimLevel.OnAimLevelCompleted += () => OnAimLevelCompleted?.Invoke();
      return aimLevel;
    }

    public void Dispose() => 
      Cleanup();
  }
}