using System;
using CodeBase.Data;
using CodeBase.Logic.PlayerLogic;
using CodeBase.Services.StaticData;
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
    [SerializeField] private FloorInformer _informer;
    
    private FirstTower _firstTower;
    private Player _player;
    
    public void Construct(FirstTower firstTower, Player player, ElevatorData elevatorData, IStaticDataService staticDataService)
    {
      _firstTower = firstTower;
      _player = player;
      _selector.Construct(elevatorData, staticDataService);
      
      _informer.Construct(staticDataService);
      _informer.SetupFloor();
    }

    public void MoveElevator(int selectedFloor, Vector3 newPosition, Action onCompleted)
    {
      SetNewPosition(newPosition);
      _shower.ShowFloorAnim(selectedFloor, () =>OpenDoor(onCompleted));
    }

    private void OpenDoor(Action onCompleted)
    {
      _finishDoor.OpenDoor();
      onCompleted.Invoke();
    }
    
    private void SetNewPosition(Vector3 newPosition)
    {
      _player.gameObject.SetActive(false);
      _player.transform.parent = transform;
      transform.position = newPosition;
      _player.transform.parent = null;
      _player.gameObject.SetActive(true);
    }
  }
}
