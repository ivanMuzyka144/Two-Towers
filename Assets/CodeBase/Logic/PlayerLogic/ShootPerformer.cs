using System;
using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Logic.Shoot;
using CodeBase.Services.Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Logic.PlayerLogic
{
    public class ShootPerformer : MonoBehaviour
    {
        [SerializeField] private PlayerRaycaster _raycaster;

        private Weapon _weapon;

        public void Construct(IInputService inputService)
        {
            
        }

        public void SetupWeapon(Weapon weapon) => 
            _weapon = weapon;

        void Update()
        {
            if (CanShoot()) 
                PerformShooting();
        }

        private bool CanShoot() => 
            Input.GetKeyDown(KeyCode.Mouse1) && _weapon != null;

        private void PerformShooting()
        {
            ShootRaycastResult shootRaycastResult = _raycaster.GetRaycastHit();
            _weapon.PerformShoot(shootRaycastResult);
        }

        
    }
}
