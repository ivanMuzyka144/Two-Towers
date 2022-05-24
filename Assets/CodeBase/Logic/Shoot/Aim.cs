using System;
using UnityEngine;

namespace CodeBase.Logic.Shoot
{
  public class Aim : MonoBehaviour, IAimable
  {
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material _aimedMaterial;

    public bool HasHitted => _hasHitted;
    public event Action OnAimHit;

    private bool _hasHitted;

    public void Hit()
    {
      if (!_hasHitted)
      {
        _hasHitted = true;
        _meshRenderer.material = _aimedMaterial;
        OnAimHit?.Invoke();
      }  
    }
  }
}
