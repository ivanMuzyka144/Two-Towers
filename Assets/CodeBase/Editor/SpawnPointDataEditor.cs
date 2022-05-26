using CodeBase.Data;
using CodeBase.StaticData;
using CodeBase.StaticData.Markers;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Editor
{
  [CustomEditor(typeof(SpawnPointData))]
  public class SpawnPointDataEditor: UnityEditor.Editor
  {
    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      SpawnPointData spawnPointData = (SpawnPointData)target;

      if (GUILayout.Button("Collect"))
      {
        spawnPointData.PlayerPoint = GetPlayerSpawnPoint();
        spawnPointData.FirstTowerPoint = GetFirstTowerSpawnPoint();
        spawnPointData.SecondTowerPoint = GetSecondTowerSpawnPoint();
      }
      
      EditorUtility.SetDirty(target);
    }
    private SpawnPoint GetPlayerSpawnPoint()
    {
      Transform playerSpawnTransform = FindObjectOfType<PlayerSpawnMarker>().transform;
      return new SpawnPoint(playerSpawnTransform.position, playerSpawnTransform.rotation);
    }
    
    private SpawnPoint GetFirstTowerSpawnPoint()
    {
      Transform firstTowerTransform = FindObjectOfType<FirstTowerMarker>().transform;
      return new SpawnPoint(firstTowerTransform.position, firstTowerTransform.rotation);
    }
    
    private SpawnPoint GetSecondTowerSpawnPoint()
    {
      Transform secondTowerSpawnTransform = FindObjectOfType<SecondTowerMarker>().transform;
      return new SpawnPoint(secondTowerSpawnTransform.position, secondTowerSpawnTransform.rotation);
    }
    
  }
}
