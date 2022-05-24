using UnityEngine;

namespace CodeBase.Data
{
  public class ShootRaycastResult
  {
    public Vector3 HitPoint;
    public Transform Target;

    public ShootRaycastResult(Vector3 hitPoint, Transform target)
    {
      HitPoint = hitPoint;
      Target = target;
    }
  }
}