using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Window
{
   public class LevelCompletedWindow : WindowBase
   {
      [SerializeField] private Button _restartButton;
      
      public void Construct(Action restartAction) => 
         _restartButton.onClick.AddListener(restartAction.Invoke);


      private void OnDestroy() => 
         _restartButton.onClick.RemoveAllListeners();
   }
}
