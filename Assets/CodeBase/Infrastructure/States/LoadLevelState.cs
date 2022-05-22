using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.Logic.Tower;
using CodeBase.Services.PersistentProgress;
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

    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, 
      IGameFactory factory, IPersistentProgressService progressService)
    {
      _stateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
      _factory = factory;
      _progressService = progressService;
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
      InitTowers();
    }

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
      
      Tower tower = _factory.CreateTower(firstTowerSpawnPoint.transform.position);
      int howManyFloors = 4;

      for (int i = 0; i < howManyFloors; i++)
      {
        Room room = _factory.CreateRoom(tower.transform);
        room.Construct(i==0, generatedColors[i]);
        tower.AddRoom(room);
      }
      tower.SetupRooms();
    }

    private void InitSecondTower(Color[] generatedColors)
    {
      
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