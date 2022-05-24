using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.PlayerLogic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Logic.Shoot
{
  public class Weapon : MonoBehaviour
  {
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _weaponPower;
    [SerializeField] private TakableWeaponModel _takableModel;
    
    private IGameFactory _factory;

    public void Construct(IGameFactory factory, Player player)
    {
      _factory = factory;
      _takableModel.Construct(player);
    }

    public void PerformShoot(ShootRaycastResult shootRaycastResult)
    {
      Vector3 direction = GetDirection(shootRaycastResult);

      Bullet bullet =  _factory.CreateBullet(_firePoint.position, _firePoint.rotation);
      bullet.Init(direction, _weaponPower);
    }

    private Vector3 GetDirection(ShootRaycastResult shootRaycastResult)
    {
      if (shootRaycastResult != null)
        return (shootRaycastResult.HitPoint - _firePoint.position).normalized;
      else
        return Camera.main.transform.forward;
    }
  }
}