using UnityEngine;

namespace CodeBase.Logic.Tower
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private RoomPresenter _roomPresenter;

        public RoomPresenter RoomPresenter => _roomPresenter;
        public void Construct(bool isFirst, Color roomColor)
        {
            _roomPresenter.SetupColor(roomColor);
            _roomPresenter.SetupArchitecture(isFirst);
        }
    
    
    }
}