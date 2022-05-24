using System;
using CodeBase.Logic.PlayerLogic;
using UnityEngine;

namespace CodeBase.Logic.Shoot
{
  public class TakableWeaponModel : MonoBehaviour,ISelectable
  {
    [SerializeField] private Collider _collider;
    
    [SerializeField] private Player _player;
    private bool _hasSelected;

  

    public void Construct(Player player)
    {
      _player = player;
    }
    public void Select()
    {
      if (!_hasSelected)
      {
        _hasSelected = true;
        _collider.enabled = false;
        var weapon = GetComponent<Weapon>();
        _player.GetComponent<ShootPerformer>().SetupWeapon(weapon);  
        _player.GetComponent<WeaponModelHolder>().SetupWeaponModel(transform);  
      }
      
      
    }
  }
}