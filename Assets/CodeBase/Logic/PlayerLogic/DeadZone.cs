using UnityEngine;

namespace CodeBase.Logic.PlayerLogic
{
  public class DeadZone : MonoBehaviour
  {
    private void OnTriggerEnter(Collider other)
    {
      if (other.TryGetComponent(out Death death))
      {
        death.Die();
      }
    }
  }
}
