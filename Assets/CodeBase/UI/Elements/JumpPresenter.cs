using CodeBase.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements
{
  public class JumpPresenter : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _jumpCountValueText;
    
    private IPersistentProgressService _progressService;

    public void Construct(IPersistentProgressService progressService)
    {
      _progressService = progressService;

      _progressService.Progress.JumpCountChanged += SetupJumpCountText;
      SetupJumpCountText();
    }

    private void SetupJumpCountText()
    {
      int count = _progressService.Progress.JumpCount;
      _jumpCountValueText.text = count.ToString();
    }
  }
}
