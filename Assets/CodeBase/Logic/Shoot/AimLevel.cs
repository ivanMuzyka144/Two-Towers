using System;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.PlayerLogic;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Logic.Shoot
{
  public class AimLevel : MonoBehaviour
  {
    [SerializeField] private int _id;
    [SerializeField] private Aim[] _aims;
    [SerializeField] private Weapon _weapon;
    public int ID => _id;
    public event Action OnAimLevelCompleted;

    private IGameFactory _factory;
    private IPersistentProgressService _progressService;

    public void Construct(IGameFactory factory, Player player, IPersistentProgressService progressService)
    {
      _factory = factory;
      _progressService = progressService;
      _weapon.Construct(factory, player);
      SubscribeToAims();
    }

    private void OnDestroy() =>
      UnsubscribeFromAims();

    private void SubscribeToAims()
    {
      foreach (Aim aim in _aims)
      {
        aim.OnAimHit += CheckIsLevelCompleted;
        aim.OnAimHit += RegisterAimHit;
      }
    }

    private void UnsubscribeFromAims()
    {
      foreach (Aim aim in _aims)
      {
        aim.OnAimHit -= CheckIsLevelCompleted;
        aim.OnAimHit -= RegisterAimHit;
      }
    }

    public void CheckIsLevelCompleted()
    {
      int completedBoxCount = 0;

      foreach (Aim aim in _aims)
      {
        if (aim.HasHitted)
          completedBoxCount++;
      }

      if (completedBoxCount == _aims.Length)
        OnAimLevelCompleted?.Invoke();
    }

    private void RegisterAimHit() => 
      _progressService.Progress.AimCount++;
  }
}