using System;
using CodeBase.Data;
using CodeBase.Services.InputServiceLogic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Logic.PlayerLogic
{
  public class PlayerRaycaster : MonoBehaviour
  {
    private readonly Vector3 _rayOrigin = new Vector3(0.5f, 0.5f, 0f);

    private IInputService _inputService;
    private Camera _camera;

    private bool _setup;
    
    public void Construct(Camera cam, IInputService inputService)
    {
      _inputService = inputService;
      _camera = cam;
      _setup = true;
    }
    private void Update()
    {
      if(_setup && _inputService.LeftMousePressed)
        Raycast();
    }

    private void Raycast()
    {
      Ray ray = _camera.ViewportPointToRay(_rayOrigin);
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit)) 
        HandleHit(hit);
    }

    private void HandleHit(RaycastHit hit)
    {
      if(hit.collider.gameObject.TryGetComponent(out ISelectable selectable))
        selectable.Select();
    }

    public ShootRaycastResult GetRaycastHit()
    {
      Ray ray = _camera.ViewportPointToRay(_rayOrigin);
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit))
        return new ShootRaycastResult(hit.point, hit.transform);
      
      return null;
    }
  }
}