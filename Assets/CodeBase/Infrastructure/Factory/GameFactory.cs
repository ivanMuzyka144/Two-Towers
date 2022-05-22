using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic.Tower;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

    private readonly IAssetProvider _assets;
    private readonly IPersistentProgressService _persistentProgressService;

    public GameFactory(IAssetProvider assets, IPersistentProgressService persistentProgressService)
    {
      _assets = assets;
      _persistentProgressService = persistentProgressService;
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

    public Elevator CreateElevator(Vector3 at) => 
      _assets.Instantiate(AssetPath.ElevatorPath, at).GetComponent<Elevator>();

    public Room CreateRoom(Transform parent)=> 
      _assets.Instantiate(AssetPath.RoomPath, parent).GetComponent<Room>();

    public Tower CreateTower(Vector3 at)
    {
      Tower tower = _assets.Instantiate(AssetPath.TowerPath, at).GetComponent<Tower>();
      return tower;
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