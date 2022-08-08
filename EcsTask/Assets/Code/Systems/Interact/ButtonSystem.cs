using Code.Components;
using Leopotam.EcsLite;

namespace Code.Systems.Interact
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
            var unitFilter = _world.Filter<MoveComponent>().End();
            var unitPool = _world.GetPool<MoveComponent>();

            var buttonFilter = _world.Filter<ButtonComponent>().End();
            var buttonPool = _world.GetPool<ButtonComponent>();

            var interactPool = _world.GetPool<InteractComponent>();

            foreach (var unitEntity in unitFilter)
            {
                ref MoveComponent move = ref unitPool.Get(unitEntity);

                foreach (var buttonEntity in buttonFilter)
                {
                    ref ButtonComponent button = ref buttonPool.Get(buttonEntity);

                    var currentDistance = (move.TransformMoved.position - button.Owner.position).sqrMagnitude;

                    if (currentDistance < button.DistanceInteract && 
                        !interactPool.Has(buttonEntity))
                    {
                        interactPool.Add(buttonEntity);

                        ref InteractComponent interact = ref interactPool.Get(buttonEntity);
                        
                        interact.Interact = button.Interact;
                        interact.DurationInteract = button.DurationMoveInteract;
                        interact.MoveInteract = button.MoveInteract;
                    }
                    
                    else if (currentDistance > buttonEntity && interactPool.Has(buttonEntity))
                    {
                        interactPool.Del(buttonEntity);
                    }
                }
            }
        }
    }
}