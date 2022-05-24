using System;
using Unity.VisualScripting;

namespace CodeBase.Logic.Shoot
{
  public interface IAimable
  {
    event Action OnAimHit; 
    void Hit();
  }
}