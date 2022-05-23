using CodeBase.Data;
using CodeBase.Logic.PlayerLogic;
using UnityEngine;

namespace CodeBase.Logic.Tower.ElevatorLogic
{
  public class Elevator : MonoBehaviour
  {
    [SerializeField] private Door _startDoor;
    [SerializeField] private Door _finishDoor;
    [Space(10)]
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
      _player.gameObject.SetActive(false);
      _player.transform.parent = transform;
      
      transform.position = newPosition;
      
      _player.transform.parent = null;
      _player.gameObject.SetActive(true);
    }
  }
}
