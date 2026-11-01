using Infrastructure.Factories.Animals;
using Infrastructure.Spawning;
using Repositories.Animals;
using Roots;
using UnityEngine;
using UseCases.Animals;
using VContainer;
using VContainer.Unity;

namespace Bootstrap
{
    public sealed class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameUIRoot _uiPrefab;
        [SerializeField] private GameWorldRoot _worldPrefab;
        [SerializeField] private AnimalsCatalog _animalsCatalog;
        [SerializeField] private AnimalsFactoryConfig _animalsFactoryConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab(_uiPrefab, Lifetime.Scoped).UnderTransform(transform);
            builder.RegisterComponentInNewPrefab(_worldPrefab, Lifetime.Scoped).UnderTransform(transform);
            
            builder.RegisterInstance<IAnimalsCatalog>(_animalsCatalog);
            builder.RegisterInstance<IAnimalsFactoryConfig>(_animalsFactoryConfig);

            builder.Register<IAnimalFactory, AnimalFactory>(Lifetime.Scoped);
            builder.Register<ISpawningPositionProvider, CameraSpawningPositionProvider>(Lifetime.Scoped);

            builder.RegisterEntryPoint<GameSceneEntryPoint>();
            builder.RegisterEntryPoint<SpawnAnimalUseCase>(Lifetime.Scoped);
        }
    }
}