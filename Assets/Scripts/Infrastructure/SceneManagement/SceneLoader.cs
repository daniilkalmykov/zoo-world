using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Repositories.ScenesManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

[assembly: InternalsVisibleTo("Assembly-CSharp")]

namespace Infrastructure.SceneManagement
{
    internal sealed class SceneLoader : ISceneLoader
    {
        private readonly LifetimeScope _parentScope;
        private readonly IScenesRepository _catalog;
        private readonly SemaphoreSlim _gate = new(1, 1);

        public SceneLoader(
            LifetimeScope parentScope,
            IScenesRepository catalog)
        {
            _parentScope = parentScope;
            _catalog = catalog;
        }

        public async UniTask LoadAsync(string sceneName, CancellationToken cancellationToken = default)
        {
            await _gate.WaitAsync(cancellationToken);

            try
            {
                await LoadCoreAsync(sceneName, cancellationToken);
            }
            finally
            {
                _gate.Release();
            }
        }

        public async UniTask UnloadAsync(string sceneName, CancellationToken cancellationToken = default)
        {
            await _gate.WaitAsync(cancellationToken);

            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var scene = SceneManager.GetSceneByName(sceneName);

                await UnloadCoreAsync(scene);
            }
            finally
            {
                _gate.Release();
            }
        }

        public async UniTask SwitchAsync(string sceneName, CancellationToken cancellationToken = default)
        {
            await _gate.WaitAsync(cancellationToken);

            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var previousScene = SceneManager.GetActiveScene();

                if (previousScene.name == sceneName)
                {
                    return;
                }

                await LoadCoreAsync(sceneName, cancellationToken);

                if (previousScene.IsValid() &&
                    previousScene.isLoaded &&
                    previousScene.name != SceneNames.Bootstrap)
                {
                    await UnloadCoreAsync(previousScene);
                }
            }
            finally
            {
                _gate.Release();
            }
        }

        private async UniTask LoadCoreAsync(string sceneName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(sceneName))
            {
                throw new ArgumentException("Scene name cannot be empty.", nameof(sceneName));
            }

            if (sceneName == SceneNames.Bootstrap)
            {
                throw new InvalidOperationException(
                    "Cannot load Bootstrap as a child scene.");
            }

            var scopePrefab = _catalog.GetScopePrefab(sceneName);

            var existingScene = SceneManager.GetSceneByName(sceneName);

            if (existingScene.IsValid() && existingScene.isLoaded)
            {
                ActivateScene(existingScene);
                return;
            }

            if (Application.CanStreamedLevelBeLoaded(sceneName) == false)
            {
                throw new InvalidOperationException(
                    $"Scene '{sceneName}' is not in the build.");
            }

            var operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            if (operation == null)
            {
                throw new InvalidOperationException(
                    $"Failed to start loading '{sceneName}'.");
            }

            await operation;

            var scene = SceneManager.GetSceneByName(sceneName);

            if (scene.IsValid() == false || scene.isLoaded == false)
            {
                throw new InvalidOperationException($"Scene '{sceneName}' failed to load.");
            }

            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (SceneManager.SetActiveScene(scene) == false)
                {
                    throw new InvalidOperationException(
                        $"Failed to activate '{sceneName}'.");
                }

                using (LifetimeScope.EnqueueParent(_parentScope))
                {
                    var scope = UnityEngine.Object.Instantiate(scopePrefab);

                    if (scope.gameObject.scene != scene)
                    {
                        SceneManager.MoveGameObjectToScene(scope.gameObject, scene);
                    }
                }

                cancellationToken.ThrowIfCancellationRequested();
            }
            catch
            {
                await UnloadCoreAsync(scene);
                throw;
            }
        }

        private static async UniTask UnloadCoreAsync(Scene scene)
        {
            if (scene.IsValid() == false || scene.isLoaded == false)
            {
                return;
            }

            if (scene.name == SceneNames.Bootstrap)
            {
                throw new InvalidOperationException("Bootstrap scene cannot be unloaded.");
            }

            if (SceneManager.GetActiveScene() == scene)
            {
                var bootstrapScene = SceneManager.GetSceneByName(
                    SceneNames.Bootstrap);

                if (bootstrapScene.IsValid() == false ||
                    bootstrapScene.isLoaded == false ||
                    SceneManager.SetActiveScene(bootstrapScene) == false)
                {
                    throw new InvalidOperationException("Cannot activate Bootstrap before unloading.");
                }
            }

            var operation = SceneManager.UnloadSceneAsync(scene);

            if (operation == null)
            {
                throw new InvalidOperationException($"Could not unload scene '{scene.name}'.");
            }

            await operation;
        }

        private static void ActivateScene(Scene scene)
        {
            if (SceneManager.SetActiveScene(scene) == false)
            {
                throw new InvalidOperationException($"Could not activate scene '{scene.name}'.");
            }
        }
    }
}