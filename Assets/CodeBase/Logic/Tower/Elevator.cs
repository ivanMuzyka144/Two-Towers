using CodeBase.Logic.PlayerLogic;
using UnityEngine;

namespace CodeBase.Logic.Tower
{
  public class Elevator : MonoBehaviour
  {
    private Tower _tower;
    private Player _player;
    public void Construct(Tower tower, Player player)
    {
      _tower = tower;
      _player = player;
    }

    public void MoveElevatorToFloor()
    {
      
    }
  }
}
