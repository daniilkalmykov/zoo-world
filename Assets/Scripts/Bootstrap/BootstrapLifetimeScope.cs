using Infrastructure.SceneManagement;
using Repositories.ScenesManagement;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Bootstrap
{
    internal sealed class BootstrapLifetimeScope : LifetimeScope
    {
        [SerializeField] private ScenesRepository _scenesRepository;
        
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterInstance<IScenesRepository>(_scenesRepository);

            builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<BootstrapEntryPoint>();

            builder.RegisterEntryPointExceptionHandler(Debug.LogException);
        }
    }
}
