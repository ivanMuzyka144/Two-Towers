using System.Collections.Generic;
using CodeBase.Logic.Tower;
using CodeBase.StaticData.Windows;
using UnityEngine;

namespace CodeBase.StaticData
{
  [CreateAssetMenu(menuName = "Static Data/Create Obstacle Course Data", fileName = "ObstacleCourseData")]
  public class ObstacleCourseData : ScriptableObject
  {
    public List<ObstacleCourse> ObstacleCoursePrefabs;
  }
}