using Code.Systems.Button;
using Code.Systems.Player;
using Code.View;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code
{
    
    sealed class EcsStartup : MonoBehaviour, IEcsWorld
    {
        private EcsWorld _world;

        private IEcsSystems _initSystems;
        private IEcsSystems _updateSystems;
        private IEcsSystems _lateUpdateSystems;

        private void Start()
        {
            _world = new EcsWorld();

            _initSystems = new EcsSystems(_world);
            _initSystems
                .Add(new ButtonInitSystem());

            _updateSystems = new EcsSystems(_world);
            _updateSystems
                .Add(new InputSystem())
                .Add(new MovementSystem())
                .Add(new ButtonSystem());

            _lateUpdateSystems = new EcsSystems(_world);
            _lateUpdateSystems.Add(new CameraSystem());

#if UNITY_EDITOR

            _initSystems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
            _updateSystems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
            _lateUpdateSystems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());

#endif


            _initSystems.Init();
            _updateSystems.Init();
            _lateUpdateSystems.Init();
        }

        private void Update()
        {
            // process systems here.
            _updateSystems?.Run();
        }

        private void LateUpdate()
        {
            _lateUpdateSystems?.Run();
        }

        private void OnDestroy()
        {
            if (_initSystems != null)
            {
                // list of custom worlds will be cleared
                // during IEcsSystems.Destroy(). so, you
                // need to save it here if you need.
                _initSystems.Destroy();
                _initSystems = null;
            }

            // cleanup custom worlds here.

            // cleanup default world.
            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }

        public EcsWorld GetWorld() => _world;
    }
}