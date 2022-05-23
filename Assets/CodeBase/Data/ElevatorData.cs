using System;

namespace CodeBase.Data
{
  public class ElevatorData
  {
    public int SelectedFloor { get; private set; }

    public Action FloorSelected;

    public void SelectFloor(int floorNumb)
    {
      SelectedFloor = floorNumb;
      FloorSelected?.Invoke();
    }
  }
}