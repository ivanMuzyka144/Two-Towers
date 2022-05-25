using System;
using CodeBase.Data;
using CodeBase.Services.SharedData;
using TMPro;
using UnityEngine;

namespace CodeBase.Logic.Tower.ElevatorLogic
{
  public class FloorSelector : MonoBehaviour
  {
    private const string ErrorName = "ERROR";
    [SerializeField] private TextMeshProUGUI _screenText;
    [Space(10)]
    [SerializeField] private FloorSelectorNumberButton[] _numberButtons;
    [SerializeField] private FloorSelectorButton _clearButton;
    [SerializeField] private FloorSelectorButton _acceptButton;
    
    private ElevatorData _elevatorData;
    private string _currString = "";

    public void Construct(ElevatorData elevatorData)
    {
      _elevatorData = elevatorData;
      SubscribeToNumberButton();
      _clearButton.OnButtonClicked += ClearNumbs;
      _acceptButton.OnButtonClicked += AcceptNumbs;
    }

    private void OnDestroy()
    {
      UnsubscribeFromNumberButton();
      _clearButton.OnButtonClicked -= ClearNumbs;
      _acceptButton.OnButtonClicked -= AcceptNumbs;
    }

    private void SubscribeToNumberButton()
    {
      foreach (FloorSelectorNumberButton numberButton in _numberButtons) 
        numberButton.OnButtonClicked += HandleNumberButtonClicked;
    }
    
    private void UnsubscribeFromNumberButton()
    {
      foreach (FloorSelectorNumberButton numberButton in _numberButtons) 
        numberButton.OnButtonClicked -= HandleNumberButtonClicked;
    }
    private void HandleNumberButtonClicked(int numb)
    {
      AppendNumb(numb);
    }

    private void AppendNumb(int numb)
    {
      if (_currString.Length >= 4)
      {
        if (_currString == ErrorName)
        {
          _currString = "";
        }
        else
        {
          _currString = ErrorName;
        }
      }
      else
      {
        _currString += numb.ToString();
      }
      _screenText.text = _currString;
    }

    private void ClearNumbs()
    {
      _currString = "";
      _screenText.text = _currString;
    }

    private void AcceptNumbs()
    {
      if (_currString == ErrorName || _currString == "")
      {
        ClearNumbs();
        return;
      }

      int currNumb = int.Parse(_currString);
      
      if (currNumb >= 4)
      {
        _currString = ErrorName;
        _screenText.text = _currString;
      }
      else
      {
        _elevatorData.SelectFloor(currNumb);
      }
    }

  }
}
