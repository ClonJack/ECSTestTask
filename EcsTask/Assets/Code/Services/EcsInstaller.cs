using UnityEngine;
using Zenject;

namespace Code.Services
{
    public class EcsInstaller : MonoInstaller
    {
        [SerializeField] private EcsStartup _ecsStartup;

        public override void InstallBindings()
        {
            BindContainer();
        }

        private void BindContainer()
        {
            Container.Bind<IEcsWorld>().FromInstance(_ecsStartup).AsSingle().NonLazy();
        }
    }
}