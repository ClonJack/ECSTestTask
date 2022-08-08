using UnityEngine;

namespace Code.Components
{
    public struct ButtonComponent
    {
        public Transform Owner;
        public Transform Interact;

        public Vector3 MoveInteract;

        public float DurationMoveInteract;
        public float DistanceInteract;
    }
}