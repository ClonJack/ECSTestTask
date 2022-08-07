using Code.Components;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Code.View
{
    public class ViewButton : MonoBehaviour
    {
        [SerializeField] private Transform _interact;
        
        [Inject] private IEcsWorld _ecsWorld;

        private EcsWorld _world;
        
        private int _entity;

        [SerializeField] private float _distance;
        [SerializeField] private float _duration;

        [SerializeField] private Vector3 _positionMove;

        private void Start()
        {
            _world = _ecsWorld.GetWorld();
            _entity = _world.NewEntity();
            
            var doorPool =  _world.GetPool<InteractComponent>();
            doorPool.Add(_entity);
            doorPool.Get(_entity).Interact = _interact;

            var buttonPool = _world.GetPool<TransformComponent>();
            buttonPool.Add(_entity);
            buttonPool.Get(_entity).Transform = transform;

            var distancePool = _world.GetPool<DistanceComponent>();
            distancePool.Add(_entity);
            distancePool.Get(_entity).Distance = _distance;

            var vectorPool = _world.GetPool<VectorComponent>();
            vectorPool.Add(_entity);
            vectorPool.Get(_entity).Position = _positionMove;

            var durationPool = _world.GetPool<DurationComponent>();
            durationPool.Add(_entity);
            durationPool.Get(_entity).Duration = _duration;
        }
    }
}