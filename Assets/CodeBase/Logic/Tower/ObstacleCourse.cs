using UnityEngine;

namespace CodeBase.Logic.Tower
{
    public class ObstacleCourse : MonoBehaviour
    {
        [SerializeField] private int _obstacleCourseID;
        public int ObstacleCourseID => _obstacleCourseID;
    }
}
