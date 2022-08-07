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


            var markerPool = _world.GetPool<MarkerComponent>();
            markerPool.Add(_entity);
            markerPool.Get(_entity).Position = transform.position;

            var targetPool = _world.GetPool<TargetComponent>();
            targetPool.Add(_entity);
            targetPool.Get(_entity).Position = transform.position;

            var cameraPool = _world.GetPool<CameraComponent>();
            cameraPool.Add(_entity);
            cameraPool.Get(_entity).Duration = _durationCamera;
        }


        private void Update()
        {
            if (_world.GetPool<MarkerComponent>().Has(_entity))
            {
                _world.GetPool<MarkerComponent>().Get(_entity).Position = transform.position;
            }

            Vector3 pointToMove = _world.GetPool<TargetComponent>().Get(_entity).Position;
            
            transform.position = Vector3.MoveTowards(transform.position, pointToMove,
                _durationMove * Time.deltaTime);
        }
    }
}