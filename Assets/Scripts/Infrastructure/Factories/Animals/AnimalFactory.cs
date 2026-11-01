using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Domain.Animals;
using Infrastructure.Random;
using Presentation.Controllers.Animals;
using Presentation.Views.Animals;
using Repositories.Animals;
using Roots;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[assembly: InternalsVisibleTo("Assembly-CSharp")]

namespace Infrastructure.Factories.Animals
{
    internal sealed class AnimalFactory : IAnimalFactory
    {
        private readonly IObjectResolver _resolver;
        private readonly IAnimalsCatalog _catalog;
        private readonly GameWorldRoot _world;
        private readonly Camera _camera;
        private readonly IRandomSource _random;
        private readonly IAnimalsFactoryConfig _spawnSettings;

        public AnimalFactory(
            IObjectResolver resolver,
            IAnimalsCatalog catalog,
            GameWorldRoot world,
            GameWorldRoot root,
            IRandomSource random,
            IAnimalsFactoryConfig spawnSettings)
        {
            _resolver = resolver;
            _catalog = catalog;
            _world = world;
            _camera = root.Camera;
            _random = random;
            _spawnSettings = spawnSettings;
        }

        public void Create(AnimalId id, Vector3 position)
        {
            var animalRepository = _catalog.AnimalRepositories.FirstOrDefault(x => x.Id == id);
            
            if (animalRepository == null)
                throw new InvalidOperationException($"Animal repository for '{id}' not found.");

            var instance = _resolver.Instantiate(
                animalRepository.Prefab,
                position,
                Quaternion.identity,
                _world.AnimalsRoot);

            try
            {
                if (instance.TryGetComponent<AnimalView>(out var view) == false)
                {
                    throw new InvalidOperationException(
                        $"Prefab '{id}' has no AnimalView.");
                }

                if (instance.TryGetComponent(out AnimalMovementController movement) == false)
                {
                    throw new InvalidOperationException(
                        $"Prefab '{id}' has no movement controller.");
                }

                view.Initialize(animalRepository.Id, animalRepository.Type);

                movement.Initialize(animalRepository.MovementConfig, _camera, _random, _spawnSettings);
            }
            catch
            {
                UnityEngine.Object.Destroy(instance);
                
                throw new InvalidOperationException("Failed to initialize animal instance.");
            }
        }
    }
}