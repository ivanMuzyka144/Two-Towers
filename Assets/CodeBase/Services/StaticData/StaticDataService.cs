using System.Collections.Generic;
using System.Linq;
using CodeBase.Data;
using CodeBase.Logic.Shoot;
using CodeBase.Logic.Tower;
using CodeBase.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private const string ObstacleCourseDataPath = "StaticData/ObstacleCourseData";
    private const string AimLevelDataPath = "StaticData/AimLevelData";
    private const string WindowsDataPath = "StaticData/UI/WindowStaticData";

    private Dictionary<int ,ObstacleCourse> _obstacleCoursePrefabs;
    private Dictionary<int,AimLevel> _aimLevelPrefabs;
    private Dictionary<WindowId,WindowConfig> _windowConfigs;
    
    public void Load()
    {
      /*
      _windowConfigs = Resources
        .Load<WindowStaticData>(WindowsDataPath)
        .Configs
        .ToDictionary(x => x.WindowId, x => x);
      */
      _obstacleCoursePrefabs = Resources
        .Load<ObstacleCourseData>(ObstacleCourseDataPath)
        .ObstacleCoursePrefabs
        .ToDictionary(x => x.ObstacleCourseID, x => x);
      
      _aimLevelPrefabs = Resources
        .Load<AimLevelData>(AimLevelDataPath)
        .AimLevelPrefabs
        .ToDictionary(x => x.ID, x => x);
    }
    public WindowConfig ForWindow(WindowId windowId) =>
      _windowConfigs.TryGetValue(windowId, out WindowConfig windowConfig)
        ? windowConfig 
        : null;
    
    public ObstacleCourse ForObstacle(int id)
    {
      int selectId = _obstacleCoursePrefabs.Keys.Contains(id) ? id : _obstacleCoursePrefabs.Keys.ToList().RandomItem();
      
      return _obstacleCoursePrefabs.TryGetValue(selectId, out ObstacleCourse prefab)
        ? prefab
        : null;
    } 
    public AimLevel ForAimLevel(int id)
    {
      int selectId = _aimLevelPrefabs.Keys.Contains(id) ? id : _aimLevelPrefabs.Keys.ToList().RandomItem();
      
      return _aimLevelPrefabs.TryGetValue(selectId, out AimLevel prefab)
        ? prefab
        : null;
    }
  }
}