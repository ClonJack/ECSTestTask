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
            var filter = _world.Filter<TargetComponent>().Inc<CameraFollowerComponent>()
                .Inc<TransformComponent>().Inc<DurationComponent>().End();

            
            var targetPool = _world.GetPool<TargetComponent>();
            var cameraPool = _world.GetPool<CameraFollowerComponent>();
            var playerPool = _world.GetPool<TransformComponent>();
            var durationPool = _world.GetPool<DurationComponent>();
            

            foreach (var entity in filter)
            {
                ref TargetComponent target = ref targetPool.Get(entity);
                ref CameraFollowerComponent cameraFollower = ref cameraPool.Get(entity);
                ref TransformComponent player = ref playerPool.Get(entity);
                ref DurationComponent duration = ref durationPool.Get(entity);
                
                Vector3 targetPosition = player.Transform.position + cameraFollower.Offset;

                _camera.position =
                    Vector3.SmoothDamp(_camera.position, targetPosition, ref cameraFollower.Velocity, cameraFollower.Duration);
            }
        }
    }
}