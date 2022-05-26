﻿using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.Logic.PlayerLogic;
using CodeBase.Logic.Tower;
using CodeBase.Logic.Tower.ElevatorLogic;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SharedData;
using CodeBase.Services.StaticData;
using CodeBase.StaticData;
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
    private readonly IStaticDataService _staticDataService;

    public LoadLevelState(GameStateMachine gameStateMachine, 
      SceneLoader sceneLoader, 
      IGameFactory factory, 
      IPersistentProgressService progressService,
      ISharedDataService sharedDataService,
      IStaticDataService staticDataService)
    {
      _stateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
      _factory = factory;
      _progressService = progressService;
      _sharedDataService = sharedDataService;
      _staticDataService = staticDataService;
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
      SpawnPoint spawnPoint = _staticDataService.PlayerSpawnPoint;
       _factory.CreateHero(spawnPoint.Position, spawnPoint.Rotation);
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
      SpawnPoint firstTowerSpawnPoint = _staticDataService.FirstTowerSpawnPoint;
      int howManyFloors = 4;//static data

      FirstTower firstTower = _factory.CreateFirstTower(firstTowerSpawnPoint.Position);

      for (int i = 0; i < howManyFloors; i++) 
        InitFirstTowerRoom(firstTower, generatedColors[i], i == 0);

      firstTower.SetupRooms();
      
      Elevator elevator = _factory.CreateElevator(firstTower.transform.position);
      firstTower.SetupElevator(elevator);
    }

    private void InitSecondTower(Color[] generatedColors)
    {
      SpawnPoint secondTowerSpawnPoint = _staticDataService.SecondTowerSpawnPoint;
      int howManyFloors = 4;//static data

      SecondTower secondTower = _factory.CreateSecondTower(secondTowerSpawnPoint.Position);

      for (int i = 0; i < howManyFloors; i++) 
        InitSecondTowerRoom(secondTower, generatedColors[i], i == 0);
      
      secondTower.SetupRooms();
    }

    private void InitFirstTowerRoom(FirstTower tower, Color colors, bool isFirst)
    {
      Room room = _factory.CreateFirstRoom(tower.transform);
      room.Construct(isFirst, colors);
      tower.AddRoom(room);
    }
    private void InitSecondTowerRoom(SecondTower tower, Color colors, bool isFirst)
    {
      Room room = _factory.CreateSecondRoom(tower.transform);
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