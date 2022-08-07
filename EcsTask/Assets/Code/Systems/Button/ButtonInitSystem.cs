using Code.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.Systems.Button
{
    public class ButtonInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            var id = 0;
            var buttonOnScene = GameObject.FindGameObjectsWithTag("Button");
            foreach (var button in buttonOnScene)
            {
                int entity = _world.NewEntity();

                var idPool = _world.GetPool<IDComponent>();
                idPool.Add(entity);
                idPool.Get(entity).Id = id;

                var buttonPool = _world.GetPool<ButtonComponent>();
                buttonPool.Add(entity);
                buttonPool.Get(entity).Button = button.transform;

                id++;
            }
        }
    }
}