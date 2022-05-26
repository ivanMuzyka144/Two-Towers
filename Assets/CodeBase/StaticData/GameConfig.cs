using UnityEngine;

namespace CodeBase.StaticData
{
  [CreateAssetMenu(menuName = "Static Data/Create Game Config", fileName = "GameConfig")]
  public class GameConfig : ScriptableObject
  {
    [Header("Tower")]
    [SerializeField] public int HowManyRooms = 4;
    [SerializeField] public float RoomHeight = 5.5f;
  }
}