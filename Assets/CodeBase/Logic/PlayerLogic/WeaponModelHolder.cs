using CodeBase.Logic.Shoot;
using CodeBase.Services.InputServiceLogic;
using UnityEngine;

namespace CodeBase.Logic.PlayerLogic
{
  public class WeaponModelHolder : MonoBehaviour
  {
    [SerializeField] private Transform _weaponModelHolder;

    public void Construct(Camera cam) => 
      _weaponModelHolder.transform.parent = cam.transform;

    public void SetupWeaponModel(Transform weaponTransform)
    {
      weaponTransform.transform.parent = _weaponModelHolder.transform;
      weaponTransform.transform.position = _weaponModelHolder.transform.position;
      weaponTransform.transform.rotation = _weaponModelHolder.transform.rotation;
    }
  }
}
