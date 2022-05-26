
using CodeBase.Logic.Shoot;
using CodeBase.Logic.Tower;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services;

namespace CodeBase.Services.StaticData
{
  public interface IStaticDataService : IService
  {
    void Load();
    WindowConfig ForWindow(WindowId levelCompleted);
    ObstacleCourse ForObstacle(int id);
    AimLevel ForAimLevel(int id);
  }
}