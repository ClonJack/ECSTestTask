using Code.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.Systems.Player
{
    public class MovementSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
        }

        public void Run(IEcsSystems systems)
        {
            var filter = _world.Filter<InputComponent>().Inc<PlayerComponent>().End();

            var inputPool = _world.GetPool<InputComponent>();
            var playerPool = _world.GetPool<PlayerComponent>();

            foreach (int entity in filter)
            {
                ref InputComponent input = ref inputPool.Get(entity);
                ref PlayerComponent player = ref playerPool.Get(entity);

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (!_world.GetPool<MovementComponent>().Has(entity))
                    {
                        _world.GetPool<MovementComponent>().Add(entity);
                        _world.GetPool<MovementComponent>().Get(entity).Duration = 15;
                    }
                }

                if (_world.GetPool<MovementComponent>().Has(entity))
                {
                    ref MovementComponent move = ref _world.GetPool<MovementComponent>().Get(entity);

                    Vector3 target = new Vector3(input.Position.x, player.Unit.position.y, input.Position.z);
                    player.Unit.position = Vector3.MoveTowards(player.Unit.position, target,
                        move.Duration * Time.deltaTime);

                    if (Vector3.Distance(player.Unit.position, input.Position) < 0.001f)
                    {
                        _world.GetPool<MovementComponent>().Del(entity);
                    }
                }
            }
        }
    }
}