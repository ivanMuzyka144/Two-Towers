using CodeBase.Services.InputServiceLogic;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SharedData;
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
    [SerializeField] private ShootPerformer _shootPerformer;
    [SerializeField] private Death _death;

    [SerializeField] private Transform _cameraPositionPoint;

    private Camera _camera;
    public void Construct(Camera cam, 
      ISharedDataService sharedDataService, 
      IInputService inputService, 
      IPersistentProgressService progressService)
    {
      _mover.Construct(inputService, progressService);
      _cameraRotator.Construct(cam, inputService);
      _raycaster.Construct(cam, inputService);
      _weaponModelHolder.Construct(cam);
      _shootPerformer.Construct(inputService, progressService);
      _death.Construct(sharedDataService);
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