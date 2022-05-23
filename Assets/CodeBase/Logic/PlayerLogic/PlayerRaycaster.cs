using System;
using UnityEngine;

namespace CodeBase.Logic.PlayerLogic
{
  public class PlayerRaycaster : MonoBehaviour
  {
    private readonly Vector3 _rayOrigin = new Vector3(0.5f, 0.5f, 0f);

    private Camera _camera;

    private bool _setup;
    
    public void Construct(Camera cam)
    {
      _camera = cam;
      _setup = true;
    }
    private void Update()
    {
      if(_setup && Input.GetKeyDown(KeyCode.Mouse0))
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
  }
}