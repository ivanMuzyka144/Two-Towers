using CodeBase.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class AimPresenter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _aimCountValueText;
    
        private IPersistentProgressService _progressService;

        public void Construct(IPersistentProgressService progressService)
        {
            _progressService = progressService;

            _progressService.Progress.AimCountChanged += SetupAimCountText;
            SetupAimCountText();
        }

        private void SetupAimCountText()
        {
            int count = _progressService.Progress.AimCount;
            _aimCountValueText.text = count.ToString();
        }
    }
}
