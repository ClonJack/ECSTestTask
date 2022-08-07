using Code.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.Systems.Player
{
    public class InputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
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


                if (Input.GetMouseButton(0))
                {
                    Vector3 point = GetPointClick(player.Transform.position);
                    target.Position = new Vector3(point.x, player.Transform.position.y, point.z);
                }
            }
        }

        private Vector3 GetPointClick(Vector3 position)
        {
            Plane playerPlane = new Plane(Vector3.up, position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (playerPlane.Raycast(ray, out var hitDistance))
            {
                return ray.GetPoint(hitDistance);
            }

            return Vector3.zero;
        }
    }
}