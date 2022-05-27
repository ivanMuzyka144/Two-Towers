using CodeBase.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements
{
  public class FirePresenter : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _fireCountValueText;
    
    private IPersistentProgressService _progressService;

    public void Construct(IPersistentProgressService progressService)
    {
      _progressService = progressService;

      _progressService.Progress.FireCountChanged += SetupFireCountText;
      SetupFireCountText();
    }

    private void SetupFireCountText()
    {
      int count = _progressService.Progress.FireCount;
      _fireCountValueText.text = count.ToString();
    }
  }
}