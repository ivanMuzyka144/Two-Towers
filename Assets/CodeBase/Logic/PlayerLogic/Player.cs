using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Logic.PlayerLogic
{
  public class Player : MonoBehaviour
  {
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private PlayerRaycaster _raycaster;
    [SerializeField] private CameraRotator _cameraRotator;
    [SerializeField] private WeaponModelHolder _weaponModelHolder;

    [SerializeField] private Transform _cameraPositionPoint;

    private Camera _camera;
    public void Construct(Camera cam)
    {
      _mover.Construct();
      _cameraRotator.Construct(cam);
      _raycaster.Construct(cam);
      _weaponModelHolder.Construct(cam);
      SetupCamera(cam);
    }

    private void SetupCamera(Camera cam)
    {
      _camera = cam;
      _camera.transform.parent = transform;
      _camera.transform.position = _cameraPositionPoint.transform.position;
    }
  }
}