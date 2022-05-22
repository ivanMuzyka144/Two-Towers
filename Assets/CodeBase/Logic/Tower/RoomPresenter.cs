using UnityEngine;

namespace CodeBase.Logic.Tower
{
  public class RoomPresenter: MonoBehaviour
  {
    [SerializeField] private MeshRenderer[] _renderers;
    
    public void SetupColor(Color color)
    {
      foreach (MeshRenderer meshRenderer in _renderers) 
        meshRenderer.material.color = color;
    }

    public void SetupArchitecture(bool isFirst, ERoomType roomType)
    {
      
    }
    
  }
}