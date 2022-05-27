using System;
using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Logic.Shoot;
using CodeBase.Services.InputServiceLogic;
using CodeBase.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Logic.PlayerLogic
{
    public class ShootPerformer : MonoBehaviour
    {
        [SerializeField] private PlayerRaycaster _raycaster;

        private IPersistentProgressService _progressService;
        private IInputService _inputService;
        private Weapon _weapon;

        public void Construct(IInputService inputService, IPersistentProgressService progressService)
        {
            _progressService = progressService;
            _inputService = inputService;
        }

        public void SetupWeapon(Weapon weapon) => 
            _weapon = weapon;

        void Update()
        {
            if (CanShoot()) 
                PerformShooting();
        }

        private bool CanShoot() => 
            _inputService.RightMousePressed && _weapon != null;

        private void PerformShooting()
        {
            ShootRaycastResult shootRaycastResult = _raycaster.GetRaycastHit();
            _weapon.PerformShoot(shootRaycastResult);
            _progressService.Progress.FireCount++;
        }

        
    }
}
