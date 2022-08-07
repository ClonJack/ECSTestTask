using Code.Components;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.Timeline;

namespace Code.Systems.Player
{
    public class CameraSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;

        private Transform _camera;


        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _camera = Camera.main.transform;
        }

        public void Run(IEcsSystems systems)
        {
            var filter = _world.Filter<MarkerComponent>().Inc<TargetComponent>().Inc<CameraComponent>().End();


            var markerPool = _world.GetPool<MarkerComponent>();
            var targetPool = _world.GetPool<TargetComponent>();
            var cameraPool = _world.GetPool<CameraComponent>();


            foreach (var entity in filter)
            {
                ref MarkerComponent marker = ref markerPool.Get(entity);
                ref TargetComponent target = ref targetPool.Get(entity);
                ref CameraComponent camera = ref cameraPool.Get(entity);

                Vector3 targetPosition = marker.Position + camera.Offset;

                _camera.position =
                    Vector3.SmoothDamp(_camera.position, targetPosition, ref camera.Velocity, camera.Duration);
            }
        }
    }
}