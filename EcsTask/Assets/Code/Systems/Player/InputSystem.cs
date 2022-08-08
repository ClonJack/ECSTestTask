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
            var filter = _world.Filter<InputComponent>().End();

            var inputPool = _world.GetPool<InputComponent>();
            var targetPool = _world.GetPool<TargetComponent>();

            if (!Input.GetMouseButton(0)) return;

            foreach (var entityInput in filter)
            {
                ref InputComponent input = ref inputPool.Get(entityInput);

                if (!targetPool.Has(entityInput))
                {
                    targetPool.Add(entityInput);
                }

                ref TargetComponent target = ref targetPool.Get(entityInput);

                target.Position = GetPointClick(input.Owner.position);
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