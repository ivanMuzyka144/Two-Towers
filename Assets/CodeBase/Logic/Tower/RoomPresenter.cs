using UnityEngine;

namespace CodeBase.Logic.Tower
{
  public class RoomPresenter: MonoBehaviour
  {
    [SerializeField] private ERoomType _roomType;
    [Space(10)]
    [SerializeField] private MeshRenderer[] _renderers;
    [Space(10)]
    [SerializeField] private GameObject _regularFrontWall;
    [SerializeField] private GameObject _doorFrontWall;
    [SerializeField] private GameObject _regularBackWall;
    [SerializeField] private GameObject _doorBackWall;
    public void SetupColor(Color color)
    {
      foreach (MeshRenderer meshRenderer in _renderers) 
        meshRenderer.material.color = color;
    }

    public void SetupArchitecture(bool isFirst)
    {
      if (_roomType == ERoomType.FirstTowerRoom)
        SetupFirstTowerRoom(isFirst);
      else if(_roomType == ERoomType.SecondTowerRoom) 
        SetupSecondTowerRoom();

    }

    private void SetupFirstTowerRoom(bool isFirst)
    {
      _regularFrontWall.SetActive(!isFirst);
      _doorFrontWall.SetActive(isFirst);
      _regularBackWall.SetActive(false);
      _doorBackWall.SetActive(true);
    }

    private void SetupSecondTowerRoom()
    {
      _regularFrontWall.SetActive(false);
      _doorFrontWall.SetActive(true);
      _regularBackWall.SetActive(true);
      _doorBackWall.SetActive(false);
    }
  }
}