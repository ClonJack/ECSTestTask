using UnityEngine;

namespace Code.Components
{
    public struct CameraFollowerComponent
    {
        public Vector3 Velocity;
        public Vector3 Offset;
        
        public Transform Target;
        
        public float Duration;
    }
}