using System;
using UnityEngine;

namespace CodeBase.Data
{
  [Serializable]
  public class PlayerProgress
  {
    [SerializeField] 
    private int _jump;
    
    [SerializeField] 
    private int _aim;
    
    [SerializeField] 
    private int _fire;

    public Action JumpCountChanged;
    public Action AimCountChanged;
    public Action FireCountChanged;
    public int JumpCount
    {
      get => _jump;
      set
      {
        _jump = value;
        JumpCountChanged.Invoke();
      }
    }
    
    public int AimCount
    {
      get => _aim;
      set
      {
        _aim = value;
        AimCountChanged.Invoke();
      }
    }
    public int FireCount
    {
      get => _fire;
      set
      {
        _fire = value;
        FireCountChanged.Invoke();
      }
    }
    [SerializeField]
    public float TimeFromStartGame;

    [SerializeField]
    public float AllTimeInGame;
  }
}