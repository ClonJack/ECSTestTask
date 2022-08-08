using Code.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.Systems.Player
{
    public class MoveSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;

        private const float MaxDistancePoint = 0.1f;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
        }

        public void Run(IEcsSystems systems)
        {
            var filter = _world.Filter<MoveComponent>().End();

            var movePool = _world.GetPool<MoveComponent>();
            var targetPool = _world.GetPool<TargetComponent>();

            foreach (var entityMove in filter)
            {
                ref MoveComponent move = ref movePool.Get(entityMove);

                if (!targetPool.Has(entityMove)) continue;

                ref TargetComponent target = ref targetPool.Get(entityMove);

                var currentDistance = (move.TransformMoved.position - target.Position).sqrMagnitude;
                if (currentDistance < MaxDistancePoint)
                {
                    targetPool.Del(entityMove);
                }

                var time = move.DurationMoved * Time.deltaTime;
                target.Position = new Vector3(target.Position.x, move.TransformMoved.position.y, target.Position.z);

                move.TransformMoved.position =
                    Vector3.MoveTowards(move.TransformMoved.position, target.Position, time);
            }
        }
    }
}