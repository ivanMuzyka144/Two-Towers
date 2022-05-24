using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.Tower.ElevatorLogic;
using CodeBase.Services.SharedData;
using UnityEngine;

namespace CodeBase.Logic.Tower
{
    public class FirstTower : MonoBehaviour
    {
        private List<Room> _rooms = new List<Room>();
        private Elevator _elevator;
        private ISharedDataService _sharedData;
        private IGameFactory _factory;

        public void Construct(IGameFactory factory,ISharedDataService sharedData)
        {
            _factory = factory;
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
            int selectedFloor = _sharedData.SharedData.ElevatorData.SelectedFloor;
            Room selectedRoom = _rooms[selectedFloor];
            selectedRoom.RoomPresenter.SetupSelectedRoom();
            _factory.CreateObstacleCourse(selectedFloor, selectedRoom.transform.position.y);
            _elevator.MoveElevator(selectedFloor, GetPositionOfFloor(selectedFloor), OnElevatorAppear);
        }

        private Vector3 GetPositionOfFloor(int selectedFloor) => 
            _rooms[selectedFloor].transform.position;

        private void OnElevatorAppear()
        {
            
        }
    }
}
