using Bootstrap.Roots;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Bootstrap
{
    public sealed class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameUIRoot _uiPrefab;
        [SerializeField] private GameWorldRoot _worldPrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab(_uiPrefab, Lifetime.Scoped).UnderTransform(transform);
            builder.RegisterComponentInNewPrefab(_worldPrefab, Lifetime.Scoped).UnderTransform(transform);

            builder.RegisterEntryPoint<GameSceneEntryPoint>();
        }
    }
}