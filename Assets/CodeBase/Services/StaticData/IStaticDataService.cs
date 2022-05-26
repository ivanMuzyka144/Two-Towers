
using CodeBase.Data;
using CodeBase.Logic.Shoot;
using CodeBase.Logic.Tower;
using CodeBase.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services;

namespace CodeBase.Services.StaticData
{
  public interface IStaticDataService : IService
  {
    GameConfig GameConfig { get; }
    SpawnPoint PlayerSpawnPoint { get; }
    SpawnPoint FirstTowerSpawnPoint { get; }
    SpawnPoint SecondTowerSpawnPoint { get; }
    void Load();
    WindowConfig ForWindow(WindowId levelCompleted);
    ObstacleCourse ForObstacle(int id);
    AimLevel ForAimLevel(int id);
  }
}