using Code.Components;
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
            var playerFilter = _world.Filter<TargetComponent>().Inc<CameraFollowerComponent>()
                .Inc<TransformComponent>().Inc<DurationComponent>().End();

            var targetPoolPlayer = _world.GetPool<TargetComponent>();
            var cameraPoolPlayer = _world.GetPool<CameraFollowerComponent>();
            var playerPoolPlayer = _world.GetPool<TransformComponent>();
            var durationPoolPlayer = _world.GetPool<DurationComponent>();


            var buttonFilter = _world.Filter<InteractComponent>().Inc<TransformComponent>().Inc<DistanceComponent>()
                .Inc<VectorComponent>().Inc<DurationComponent>()
                .End();

            var targetPoolDoor = _world.GetPool<InteractComponent>();
            var targetPoolButton = _world.GetPool<TransformComponent>();
            var distancePool = _world.GetPool<DistanceComponent>();
            var vectorPool = _world.GetPool<VectorComponent>();
            var durationMoveDoorPool = _world.GetPool<DurationComponent>();


            foreach (var entityPlayer in playerFilter)
            {
                ref TargetComponent target = ref targetPoolPlayer.Get(entityPlayer);
                ref CameraFollowerComponent cameraFollower = ref cameraPoolPlayer.Get(entityPlayer);
                ref TransformComponent player = ref playerPoolPlayer.Get(entityPlayer);
                ref DurationComponent durationPlayer = ref durationPoolPlayer.Get(entityPlayer);

                foreach (var entityButton in buttonFilter)
                {
                    ref InteractComponent door = ref targetPoolDoor.Get(entityButton);
                    ref TransformComponent button = ref targetPoolButton.Get(entityButton);
                    ref DistanceComponent distance = ref distancePool.Get(entityButton);
                    ref VectorComponent moveVector = ref vectorPool.Get(entityButton);
                    ref DurationComponent durationMoveDoor = ref durationMoveDoorPool.Get(entityButton);

                    var currentDistance = (player.Transform.position - button.Transform.position).sqrMagnitude;

                    if (currentDistance < distance.Distance)
                    {
                        Transform doorMove = door.Interact;
                        doorMove.position = Vector3.MoveTowards(doorMove.position,
                            doorMove.position + moveVector.Position,
                            durationMoveDoor.Duration * Time.deltaTime);
                    }
                }
            }
        }
    }
}