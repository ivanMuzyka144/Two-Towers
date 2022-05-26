using UnityEngine;

namespace CodeBase.Logic.Tower.ObstacleElements
{
    public class RotationObstacleElement : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed;
        void Update()
        {
            transform.Rotate(transform.up, _rotationSpeed * Time.deltaTime);
        }
    }
}
