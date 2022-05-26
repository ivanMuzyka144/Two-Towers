using System;
using CodeBase.Data;
using CodeBase.Services.SharedData;
using CodeBase.Services.StaticData;
using CodeBase.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

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

    private IStaticDataService _staticDataService;
    private ElevatorData _elevatorData;
    private string _currString = "";

    private bool _isSelectionCompleted;
    public void Construct(ElevatorData elevatorData, IStaticDataService staticDataService)
    {
      _elevatorData = elevatorData;
      _staticDataService = staticDataService;
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
      if(_isSelectionCompleted)
        return;
      
      AppendNumb(numb);
    }

    private void AppendNumb(int numb)
    {
      if (_currString.Length >= _staticDataService.GameConfig.HowManyRooms)
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
      if(_isSelectionCompleted)
        return;
      
      _currString = "";
      _screenText.text = _currString;
    }

    private void AcceptNumbs()
    {
      if(_isSelectionCompleted)
        return;
      
      if (_currString == ErrorName || _currString == "")
      {
        ClearNumbs();
        return;
      }

      int currNumb = int.Parse(_currString);
      
      if (currNumb >= _staticDataService.GameConfig.HowManyRooms)
      {
        _currString = ErrorName;
        _screenText.text = _currString;
      }
      else
      {
        _currString = "->";
        _screenText.text = _currString;
        _isSelectionCompleted = true;
        _elevatorData.SelectFloor(currNumb);
      }
    }

  }
}
