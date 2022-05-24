using UnityEngine;

namespace CodeBase.Logic.Tower
{
    public class OpenCloseDoorTrigger : MonoBehaviour
    {
        [SerializeField] private Door _door;
        [SerializeField] private bool _shouldOpen;

        private bool _isActivated;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && !_isActivated)
            {
                _isActivated = true;
                
                if (_shouldOpen)
                    _door.OpenDoor();
                else
                    _door.CloseDoor();

            }
        }
    }
}
