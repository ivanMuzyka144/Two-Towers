using System.Collections.Generic;
using CodeBase.Services.SharedData;
using UnityEngine;

namespace CodeBase.Logic.Tower
{
  public class SecondTower : MonoBehaviour
  {
    private List<Room> _rooms = new List<Room>();
    
    private ISharedDataService _sharedData;
    
    public void Construct(ISharedDataService sharedData)
    {
      _sharedData = sharedData;
      _sharedData.SharedData.ElevatorData.FloorSelected += HandleFloorSelected;
    }

    private void OnDestroy()
    {
      _sharedData.SharedData.ElevatorData.FloorSelected -= HandleFloorSelected;
    }

    public void AddRoom(Room room) => 
      _rooms.Add(room);

    public void SetupRooms()
    {
      Vector3 startPos = transform.position;
      float height = 5.5f;
            
      for (int i = 0; i < _rooms.Count; i++)
      {
        Room room = _rooms[i];
        room.transform.position = startPos + new Vector3(0, i*height, 0);
      }
    }
    
    private void HandleFloorSelected()
    {
      int selectedFloor = _sharedData.SharedData.ElevatorData.SelectedFloor;
      Room selectedRoom = _rooms[selectedFloor];
      selectedRoom.RoomPresenter.SetupSelectedRoom();
    }
  }
}