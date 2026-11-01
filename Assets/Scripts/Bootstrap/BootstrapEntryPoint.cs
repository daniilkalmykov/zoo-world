using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure.SceneManagement;
using VContainer.Unity;

namespace Bootstrap
{
    internal sealed class BootstrapEntryPoint : IAsyncStartable
    {
        private readonly ISceneLoader _sceneLoader;

        public BootstrapEntryPoint(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
        }

        public async UniTask StartAsync(CancellationToken cancellationToken = new())
        {
            try
            {
                await _sceneLoader.LoadAsync(
                    SceneNames.Game,
                    cancellationToken);
            }
            catch (OperationCanceledException)
                when (cancellationToken.IsCancellationRequested)
            {
                // Expected when the Bootstrap scope is destroyed.
            }
        }
    }
}