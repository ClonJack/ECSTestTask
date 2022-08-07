using Code.Components;
using Code.View;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.Systems.Button
{
    public class ButtonSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
        }

        public void Run(IEcsSystems systems)
        {
            var buttonFilter = _world.Filter<IDComponent>()
                .Inc<ButtonComponent>()
                .End();

            var playerFilter = _world.Filter<PlayerComponent>().End();

            var buttonPool = _world.GetPool<ButtonComponent>();
            var playerPool = _world.GetPool<PlayerComponent>();

            foreach (int entityButton in buttonFilter)
            {
                ref ButtonComponent button = ref buttonPool.Get(entityButton);

                foreach (var entityPlayer in playerFilter)
                {
                    ref PlayerComponent player = ref playerPool.Get(entityPlayer);

                    var distance = (player.Unit.position - button.Button.position).sqrMagnitude;

                    if (distance < 2f)
                    {
                        if (!_world.GetPool<DoorComponent>().Has(entityButton))
                        {
                            _world.GetPool<DoorComponent>().Add(entityButton);
                            
                            _world.GetPool<DoorComponent>().Get(entityButton).Door =
                                button.Button.GetComponent<ViewButton>().Door;
                        }
                    }
                    else
                    {
                        if (_world.GetPool<DoorComponent>().Has(entityButton))
                        {
                            _world.GetPool<DoorComponent>().Del(entityButton);
                        }
                    }

                    if (_world.GetPool<DoorComponent>().Has(entityButton))
                    {
                        Transform doorMove = _world.GetPool<DoorComponent>().Get(entityButton).Door;
                        doorMove.position = Vector3.MoveTowards(doorMove.position, 
                            doorMove.position + (Vector3.up * 15),
                            5f * Time.deltaTime);
                    }
                }
            }
        }
    }
}