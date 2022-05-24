using System.Collections.Generic;
using CodeBase.Logic.PlayerLogic;
using CodeBase.Logic.Shoot;
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
    FirstTower FirstTower { get; }
    SecondTower SecondTower { get; }
    Player Hero { get; }
    void Cleanup();
    Elevator CreateElevator(Vector3 at);
    Room CreateFirstRoom(Transform parent);
    Room CreateSecondRoom(Transform parent);
    Player CreateHero(Vector3 at, Quaternion rotation);
    FirstTower CreateFirstTower(Vector3 at);
    SecondTower CreateSecondTower(Vector3 at);
    GameObject CreateHud();
    ObstacleCourse CreateObstacleCourse(int selectedFloor, float height);
    Bullet CreateBullet(Vector3 at, Quaternion rotation);
    AimLevel CreateAimLevel(Vector3 at, Quaternion rotation);
  }
}