using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.Logic.PlayerLogic;
using CodeBase.Logic.Tower;
using CodeBase.Logic.Tower.ElevatorLogic;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SharedData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private const string InitialPointTag = "InitialPoint";
    private const string EnemySpawnerTag = "EnemySpawner";

    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly IGameFactory _factory;
    private readonly IPersistentProgressService _progressService;
    private readonly ISharedDataService _sharedDataService;

    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, 
      IGameFactory factory, IPersistentProgressService progressService,
      ISharedDataService sharedDataService)
    {
      _stateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
      _factory = factory;
      _progressService = progressService;
      _sharedDataService = sharedDataService;
    }

    public void Enter(string sceneName)
    {
      _factory.Cleanup();
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() {}

    private void OnLoaded()
    {
      InitGameWorld();
      InformProgressReaders();

      _stateMachine.Enter<GameLoopState>();
    }

    private void InformProgressReaders()
    {
      foreach (ISavedProgressReader progressReader in _factory.ProgressReaders)
        progressReader.LoadProgress(_progressService.Progress);
    }

    private void InitGameWorld()
    {
      InitHero();
      InitHud();
      InitTowers();
    }

    private void InitHero()
    {
      GameObject heroSpawnPoint = GameObject.Find("HeroSpawnPoint");
       _factory.CreateHero(heroSpawnPoint.transform.position, heroSpawnPoint.transform.rotation);
    }

    private void InitHud() => 
      _factory.CreateHud();

    private void InitTowers()
    {
      int howManyFloors = 4;
      Color[] generatedColors = GetRandomColors(howManyFloors);

      InitFirstTower(generatedColors);
      InitSecondTower(generatedColors);
    }

    private void InitFirstTower(Color[] generatedColors)
    {
      GameObject firstTowerSpawnPoint = GameObject.Find("TowerSpawnPoint");
      int howManyFloors = 4;//static data

      Tower tower = _factory.CreateTower(firstTowerSpawnPoint.transform.position);

      for (int i = 0; i < howManyFloors; i++) 
        InitRoom(tower, generatedColors[i], i == 0);

      tower.SetupRooms();
      
      Elevator elevator = _factory.CreateElevator(tower.transform.position);
      
      tower.SetupElevator(elevator);
    }

    private void InitSecondTower(Color[] generatedColors)
    {
      
    }

    private void InitRoom(Tower tower, Color colors, bool isFirst)
    {
      Room room = _factory.CreateRoom(tower.transform);
      room.Construct(isFirst, colors);
      tower.AddRoom(room);
    }

    private Color[] GetRandomColors(int howManyFloors)
    {
      Color[] randomColors = new Color[howManyFloors];
      
      for (int i = 0; i < randomColors.Length; i++) 
        randomColors[i] = RandomColor();
      
      return randomColors;
    }

    private Color RandomColor()
    {
      return new Color(
        Random.Range(0f, 1f),
        Random.Range(0f, 1f),
        Random.Range(0f, 1f)
      );
    }
  }
}