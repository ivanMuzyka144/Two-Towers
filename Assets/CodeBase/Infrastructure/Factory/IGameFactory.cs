using System.Collections.Generic;
using CodeBase.Logic.PlayerLogic;
using CodeBase.Logic.Tower;
using CodeBase.Logic.Tower.ElevatorLogic;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public interface IGameFactory : IService
  {
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
    GameObject Hud { get; }
    Tower Tower { get; }
    Player Hero { get; }
    void Cleanup();
    Elevator CreateElevator(Vector3 at);
    Room CreateRoom(Transform parent);
    Tower CreateTower(Vector3 at);
    GameObject CreateHud();
    
    Player CreateHero(Vector3 at, Quaternion rotation);

  }
}