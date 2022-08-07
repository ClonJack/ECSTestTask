using Code.View;
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

            foreach (var viewUnit in FindObjectsOfType<ViewPlayer>())
            {
                Container.Bind<ViewPlayer>().FromInstance(viewUnit).NonLazy();
            }
        }
    }
}