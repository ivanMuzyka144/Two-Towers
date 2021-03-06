using DG.Tweening;
using UnityEngine;

namespace CodeBase.Logic.Tower
{
  public class Door : MonoBehaviour
  {
    [SerializeField] private Transform _leftDoor;
    [SerializeField] private Transform _rightDoor;
    [Space(10)]
    [SerializeField] private Transform _leftDoorStartPoint;
    [SerializeField] private Transform _leftDoorFinishPoint;
    [SerializeField] private Transform _rightDoorStartPoint;
    [SerializeField] private Transform _rightDoorFinishPoint;
    [Space(10)] 
    [SerializeField] private float _openCloseTime = 1;

    [Space(10)] [SerializeField] private bool _shouldOpenInStart;
    private void Start()
    {
      if(_shouldOpenInStart)
        OpenDoor();
    }

    public void OpenDoor()
    {
      _leftDoor.DOMove(_leftDoorFinishPoint.position, _openCloseTime);
      _rightDoor.DOMove(_rightDoorFinishPoint.position, _openCloseTime);
    }
  
    public void CloseDoor()
    {
      _leftDoor.DOMove(_leftDoorStartPoint.position, _openCloseTime);
      _rightDoor.DOMove(_rightDoorStartPoint.position, _openCloseTime);
    }
  
  
  }
}
