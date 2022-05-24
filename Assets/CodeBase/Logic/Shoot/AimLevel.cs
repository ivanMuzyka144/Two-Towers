using System;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.PlayerLogic;
using UnityEngine;

namespace CodeBase.Logic.Shoot
{
  public class AimLevel : MonoBehaviour
  {
    [SerializeField] private Aim[] _aims;
    [SerializeField] private Weapon _weapon;
    
    public event Action OnAimLevelCompleted;
    
    private IGameFactory _factory;

    public void Construct(IGameFactory factory, Player player)
    {
      _factory = factory;
      _weapon.Construct(factory, player);
      SubscribeToAims();
    }

    private void OnDestroy() => 
      UnsubscribeFromAims();

    private void SubscribeToAims()
    {
      foreach (Aim aim in _aims) 
        aim.OnAimHit += CheckIsLevelCompleted;
    }
    
    private void UnsubscribeFromAims()
    {
      foreach (Aim aim in _aims) 
        aim.OnAimHit += CheckIsLevelCompleted;
    }
    
    public void CheckIsLevelCompleted()
    {
      int completedBoxCount = 0;
      
      foreach (Aim aim in _aims)
      {
        if (aim.HasHitted) 
          completedBoxCount++;
      }
      
      if(completedBoxCount == _aims.Length)
        OnAimLevelCompleted?.Invoke();
    }
    
    
  }
}