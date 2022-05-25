using System;
using UnityEngine;

namespace CodeBase.Logic.Tower.ElevatorLogic
{
  public class FloorSelectorNumberButton : MonoBehaviour, ISelectable
  {
    [SerializeField] private int _buttonValue;

    public Action<int> OnButtonClicked;
    public void Select() => 
      OnButtonClicked?.Invoke(_buttonValue);
  }
}