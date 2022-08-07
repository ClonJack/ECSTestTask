using System;
using Code.Components;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Code.View
{
    public class ViewPlayer : MonoBehaviour
    {
        [Inject] private IEcsWorld _ecsWorld;

        private EcsWorld _world;

        private int _entity;

        [SerializeField] private float _durationCamera;
        [SerializeField] private float _durationMove;

        private void Start()
        {
            _world = _ecsWorld.GetWorld();

            _entity = _world.NewEntity();

            
            var targetPool = _world.GetPool<TargetComponent>();
            targetPool.Add(_entity);
            targetPool.Get(_entity).Position = transform.position;

            var cameraPool = _world.GetPool<CameraFollowerComponent>();
            cameraPool.Add(_entity);
            cameraPool.Get(_entity).Duration = _durationCamera;

            var playerPool = _world.GetPool<TransformComponent>();
            playerPool.Add(_entity);
            playerPool.Get(_entity).Transform = transform;

            var durationPool = _world.GetPool<DurationComponent>();
            durationPool.Add(_entity);
            durationPool.Get(_entity).Duration = _durationMove;

        }
    }
}