using System.Collections.Generic;
using CodeBase.Logic.Shoot;
using UnityEngine;

namespace CodeBase.StaticData
{
  [CreateAssetMenu(menuName = "Static Data/Create Aim Level Data", fileName = "CreateAimLevelData")]
  public class AimLevelData : ScriptableObject
  {
    public List<AimLevel> AimLevelPrefabs;
  }
}