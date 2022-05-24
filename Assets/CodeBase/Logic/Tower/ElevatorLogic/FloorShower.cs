using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace CodeBase.Logic.Tower.ElevatorLogic
{
    public class FloorShower : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _floorText;
        [SerializeField] private float _tickTime = 0.75f;
        public void ShowFloorAnim(int selectedFloor, Action onCompleted)
        {
            StartCoroutine(CO_ShowFloorAnim(selectedFloor, onCompleted));
        }

        private IEnumerator CO_ShowFloorAnim(int selectedFloor, Action onCompleted)
        {
            for (int i = 0; i <= selectedFloor; i++)
            {
                _floorText.text = i.ToString();
                yield return new WaitForSeconds(_tickTime);
            }
            onCompleted.Invoke();
        }
    }
}
