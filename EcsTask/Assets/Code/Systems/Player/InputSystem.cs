using Code.Components;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Code.Systems.Player
{
    public class InputSystem : IEcsInitSystem, IEcsRunSystem,IInitializable
    {
        private EcsWorld _world;

        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
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
                
                if (Input.GetMouseButton(0))
                {
                    Vector3 point = GetPointClick(marker.Position);
                    target.Position = new Vector3(point.x, marker.Position.y, point.z);

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

        public void Initialize()
        {
            
        }
    }
}