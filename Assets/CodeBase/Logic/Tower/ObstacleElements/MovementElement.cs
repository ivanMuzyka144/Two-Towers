using DG.Tweening;
using UnityEngine;

namespace CodeBase.Logic.Tower.ObstacleElements
{
    public class MovementElement : MonoBehaviour
    {
        [SerializeField] private float _distance;
        [SerializeField] private float _time;
        void Start()
        {
            Vector3 startPosition = transform.position;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOMove(startPosition + new Vector3(0,0,_distance), _time));
            sequence.Append(transform.DOMove(startPosition - new Vector3(0,0,_distance), _time));
            sequence.SetLoops(-1, LoopType.Yoyo);
        }
    }
}
