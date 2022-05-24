using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

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

  public void OpenDoor()
  {
      _leftDoor.DOMove(_leftDoorFinishPoint.position, _openCloseTime);
      _rightDoor.DOMove(_rightDoorFinishPoint.position, _openCloseTime);
  }
  
  public void CloseDoor()
  {
      _leftDoor.DOMove(_leftDoorStartPoint.position, _openCloseTime);
      _rightDoor.DOMove(_rightDoorStartPoint.position, _openCloseTime);
      Debug.Log("CLOSEE");
  }
  
  
}
