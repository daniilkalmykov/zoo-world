using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using VContainer.Unity;

[assembly: InternalsVisibleTo("Assembly-CSharp")]
namespace Repositories.ScenesManagement
{
    [CreateAssetMenu(menuName = "Zoo World/Scenes Catalog")]
    internal sealed class ScenesRepository : ScriptableObject, IScenesRepository
    {
        [Serializable]
        public sealed class SceneEntry
        {
            [field: SerializeField] public string SceneName { get; private set; }
            [field: SerializeField] public LifetimeScope ScopePrefab { get; private set; }
        }
        
        [SerializeField] private SceneEntry[] _scenes;

        public LifetimeScope GetScopePrefab(string sceneName)
        {
            foreach (var entry in _scenes)
            {
                if (entry.SceneName != sceneName)
                {
                    continue;
                }

                if (entry.ScopePrefab == null)
                {
                    throw new InvalidOperationException($"No scope prefab for scene '{sceneName}'.");
                }

                return entry.ScopePrefab;
            }

            throw new InvalidOperationException(
                $"Scene '{sceneName}' is not registered.");
        }
    }
}