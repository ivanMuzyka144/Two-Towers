using System;
using UnityEngine;

namespace CodeBase.Logic.Tower.ElevatorLogic
{
  public class FloorSelectorButton: MonoBehaviour, ISelectable
  {
    public Action OnButtonClicked;
    public void Select() => 
      OnButtonClicked?.Invoke();
  }
}