using CodeBase.Data;
using CodeBase.Services.SharedData;
using UnityEngine;

namespace CodeBase.Logic.Tower.ElevatorLogic
{
  public class FloorSelector : MonoBehaviour, ISelectable
  {
    private ElevatorData _elevatorData;

    public void Construct(ElevatorData elevatorData)
    {
      _elevatorData = elevatorData;
    }
    
    public void Select()
    {
      _elevatorData.SelectFloor(2);
    }
  }
}
