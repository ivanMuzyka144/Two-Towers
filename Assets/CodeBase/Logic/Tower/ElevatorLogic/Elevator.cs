using CodeBase.Data;
using CodeBase.Logic.PlayerLogic;
using UnityEngine;

namespace CodeBase.Logic.Tower.ElevatorLogic
{
  public class Elevator : MonoBehaviour
  {
    [SerializeField] private FloorSelector _selector;
    [SerializeField] private FloorShower _shower;
    
    private Tower _tower;
    private Player _player;
    
    public void Construct(Tower tower, Player player, ElevatorData elevatorData)
    {
      _tower = tower;
      _player = player;
      _selector.Construct(elevatorData);
    }

    public void MoveElevator(Vector3 newPosition)
    {
      
    }
  }
}
