using Code.Components;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Code.View
{
    public class ViewPlayer : MonoBehaviour
    {
        [Inject] private IEcsWorld _ecsWorld;

        private EcsWorld _world;

        private int _entity;

        [SerializeField] private float _durationCamera;
        [SerializeField] private float _durationMove;

        private void Start()
        {
            _world = _ecsWorld.GetWorld();

            _entity = _world.NewEntity();

            var movePool = _world.GetPool<MoveComponent>();
            movePool.Add(_entity);

            ref MoveComponent move = ref movePool.Get(_entity);
            move.TransformMoved = transform;
            move.DurationMoved = _durationMove;

            var targetPool = _world.GetPool<InputComponent>();
            targetPool.Add(_entity);
            
            ref InputComponent input = ref targetPool.Get(_entity);
            input.Owner = transform;

            var cameraFollowerPool = _world.GetPool<CameraFollowerComponent>();
            cameraFollowerPool.Add(_entity);
            
            ref CameraFollowerComponent camera = ref cameraFollowerPool.Get(_entity);
            camera.Duration = _durationCamera;
            camera.Target = transform;
        }
    }
}