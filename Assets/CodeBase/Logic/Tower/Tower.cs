using System;
using System.Collections.Generic;
using CodeBase.Logic.Tower.ElevatorLogic;
using CodeBase.Services.SharedData;
using UnityEngine;

namespace CodeBase.Logic.Tower
{
    public class Tower : MonoBehaviour
    {
        private List<Room> _rooms = new List<Room>();
        private Elevator _elevator;
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

        public void SetupElevator(Elevator elevator)
        {
            _elevator = elevator;
        }

        private void HandleFloorSelected()
        {
            //spawn barier
            int selectedFloor = _sharedData.SharedData.ElevatorData.SelectedFloor;
            _elevator.MoveElevator(selectedFloor, GetPositionOfFloor(selectedFloor), OnElevatorAppear);
        }

        private Vector3 GetPositionOfFloor(int selectedFloor) => 
            _rooms[selectedFloor].transform.position;

        private void OnElevatorAppear()
        {
            
        }
    }
}
