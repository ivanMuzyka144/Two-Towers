using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Logic.Tower
{
    public class Tower : MonoBehaviour
    {
        private List<Room> _rooms = new List<Room>();
        private Elevator _elevator;
        
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
    }
}
