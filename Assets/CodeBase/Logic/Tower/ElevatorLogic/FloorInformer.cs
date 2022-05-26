using CodeBase.Services.StaticData;
using TMPro;
using UnityEngine;

namespace CodeBase.Logic.Tower.ElevatorLogic
{
    public class FloorInformer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _floorText;
    
        private IStaticDataService _staticData;

        public void Construct(IStaticDataService staticData)
        {
            _staticData = staticData;
        }

        public void SetupFloor()
        {
            int maxFloor = _staticData.GameConfig.HowManyRooms - 1;
            _floorText.text = $"{maxFloor}, (0-{maxFloor})";
        }
    }
}
