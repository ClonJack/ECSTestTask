using Code.Systems.Interact;
using Code.Systems.Player;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code
{
    sealed class EcsStartup : MonoBehaviour, IEcsWorld
    {
        private EcsWorld _world;

        private IEcsSystems _updateSystems;
        private IEcsSystems _lateUpdateSystems;

        private void Start()
        {
            _world = new EcsWorld();

            _updateSystems = new EcsSystems(_world);
            _updateSystems
                .Add(new InputSystem())
                .Add(new MoveSystem())
                .Add(new ButtonSystem())
                .Add(new InteractSystem());

            _lateUpdateSystems = new EcsSystems(_world);
            _lateUpdateSystems.Add(new CameraSystem());

#if UNITY_EDITOR


            _updateSystems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
            _lateUpdateSystems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());

#endif


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
            _updateSystems.Destroy();
            _lateUpdateSystems.Destroy();
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