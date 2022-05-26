using CodeBase.Data;
using UnityEngine;

namespace CodeBase.StaticData
{
  [CreateAssetMenu(menuName = "Static Data/Create Spawn Point Data", fileName = "SpawnPointData")]
  public class SpawnPointData : ScriptableObject
  {
    public SpawnPoint PlayerPoint;
    public SpawnPoint FirstTowerPoint;
    public SpawnPoint SecondTowerPoint;
  }
}