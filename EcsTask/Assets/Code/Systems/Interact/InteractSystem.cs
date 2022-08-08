using Code.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.Systems.Interact
{
    public class InteractSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
        }

        public void Run(IEcsSystems systems)
        {
            var filterInteract = _world.Filter<InteractComponent>().End();
            
            var interactPool = _world.GetPool<InteractComponent>();

            foreach (var entityInteract in filterInteract)
            {
                ref InteractComponent interact = ref interactPool.Get(entityInteract);
                Vector3 targetMove = interact.Interact.position + interact.MoveInteract;
                float time = interact.DurationInteract * Time.deltaTime;

                interact.Interact.position =
                    Vector3.MoveTowards(interact.Interact.position, targetMove, time);
              
            }
        }
    }
}