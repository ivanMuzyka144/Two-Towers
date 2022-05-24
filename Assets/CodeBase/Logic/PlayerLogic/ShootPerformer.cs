using System;
using System.Collections.Generic;
using CodeBase.Data;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Logic.PlayerLogic
{
    public class ShootPerformer : MonoBehaviour
    {
        [SerializeField] private PlayerRaycaster _raycaster;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private Transform _bulletPrefab;
        [SerializeField] private float bulletSpeed;

        private List<Vector3> points = new List<Vector3>();
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                PerformShooting();
            }
        }

        private void PerformShooting()
        {
            ShootRaycastResult shootRaycastResult = _raycaster.GetRaycastHit();

            if (shootRaycastResult != null)
                ShootInTarger(shootRaycastResult.HitPoint);
            else
                ShootWithoutTarget();
        }

        private void ShootInTarger(Vector3 targerPoint)
        {
            Vector3 direction = (targerPoint - _firePoint.position).normalized;
            Rigidbody bulletClone =  Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation).GetComponent<Rigidbody>();
            bulletClone.velocity = direction  * bulletSpeed;
        }

        private void ShootWithoutTarget()
        {
            Rigidbody bulletClone =  Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation).GetComponent<Rigidbody>();
            bulletClone.velocity = Camera.main.transform.forward  * bulletSpeed;
        }

        private void OnDrawGizmos()
        {
            foreach (Vector3 point in points)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(point,1);
            }
        }
    }
}
