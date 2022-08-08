using Code.Components;
using Leopotam.EcsLite;
using UnityEngine;

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
            var filter = _world.Filter<CameraFollowerComponent>().End();

            var cameraPool = _world.GetPool<CameraFollowerComponent>();

            foreach (var entity in filter)
            {
                ref CameraFollowerComponent cameraFollower = ref cameraPool.Get(entity);

                var targetPosition = cameraFollower.Target.position + cameraFollower.Offset;

                _camera.position =
                    Vector3.SmoothDamp(_camera.position, targetPosition, ref cameraFollower.Velocity,
                        cameraFollower.Duration);
            }
        }
    }
}