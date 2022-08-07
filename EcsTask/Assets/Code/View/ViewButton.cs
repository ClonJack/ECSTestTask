using UnityEngine;

namespace Code.View
{
    public class ViewButton : MonoBehaviour
    {
        [SerializeField] private Transform _door;
        
        public Transform Door => _door;
    }
}