using Code.Components;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Code.View
{
    public class ViewButton : MonoBehaviour
    {
        [Inject] private IEcsWorld _ecsWorld;
        private EcsWorld _world;
        private int _entity;
        
        [SerializeField] private Transform _interact;

        [SerializeField] private Vector3 _positionMove;

        [SerializeField] private float _distance;
        [SerializeField] private float _duration;

        private void Start()
        {
            _world = _ecsWorld.GetWorld();
            _entity = _world.NewEntity();
            
            var buttonPool = _world.GetPool<ButtonComponent>();
            buttonPool.Add(_entity);

            ref ButtonComponent button = ref buttonPool.Get(_entity);

            button.DistanceInteract = _distance;
            button.Owner = transform;
            button.Interact = _interact;
            button.MoveInteract = _positionMove;
            button.DurationMoveInteract = _duration;
        }
    }
}