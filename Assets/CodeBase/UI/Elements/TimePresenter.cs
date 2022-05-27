using System;
using CodeBase.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements
{
  public class TimePresenter : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _timeValueText;
    
    private IPersistentProgressService _progressService;
    private float _startGameTime;

    public void Construct(IPersistentProgressService progressService)
    {
      _progressService = progressService;
      _startGameTime = _progressService.Progress.TimeFromStartGame;
    }

    private void Update() => 
      SetupTimeText();

    private void SetupTimeText()
    {
      _timeValueText.text = FormatTime( Time.time - _startGameTime);
    }

    private string FormatTime( float time )
    {
      TimeSpan timeSpan = TimeSpan.FromSeconds(time);
      return string.Format("{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
    }
  }
}
