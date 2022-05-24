using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.Logic.Shoot
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _lifetime;
        [SerializeField] private Rigidbody _rigidbody;

        public void Init(Vector3 direction, float bulletVelocity)
        {
            _rigidbody.velocity = direction * bulletVelocity;
            StartCoroutine(CO_DestroyBullet());
        }

        private IEnumerator CO_DestroyBullet()
        {
            yield return new WaitForSeconds(_lifetime);
            DestroyBullet();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IAimable aimable))
            {
                aimable.Hit();
            }
            DestroyBullet();
        }

        private void DestroyBullet() => 
            Destroy(gameObject);
    }
}
