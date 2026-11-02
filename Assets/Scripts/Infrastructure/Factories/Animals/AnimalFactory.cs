using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Domain.Interfaces;
using Domain.Models.Animals;
using Infrastructure.Random;
using Presentation.Controllers.Animals;
using Presentation.Views.Animals;
using Presentation.Views.Decorator;
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
            IRandomSource random,
            IAnimalsFactoryConfig spawnSettings)
        {
            _resolver = resolver;
            _catalog = catalog;
            _world = world;
            _camera = world.Camera;
            _random = random;
            _spawnSettings = spawnSettings;
        }

        public event Action<IAnimalCollisionController> ControllerCreated;

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
                    throw new InvalidOperationException($"Prefab '{id}' has no AnimalView.");
                }

                if (instance.TryGetComponent(out AnimalMovementController movement) == false)
                {
                    throw new InvalidOperationException($"Prefab '{id}' has no movement controller.");
                }
                
                if (instance.TryGetComponent(out IColliderDecoratorView colliderDecoratorView) == false)
                {
                    throw new InvalidOperationException($"Prefab '{id}' has no collider decorator view.");
                }

                var animalCollisionController = new AnimalCollisionController(view, colliderDecoratorView);
                
                ControllerCreated?.Invoke(animalCollisionController);
                
                IAnimal animal = new Animal(animalRepository.Type, animalRepository.Id, Guid.NewGuid());

                view.Initialize(animal);
                movement.Initialize(animalRepository.MovementConfig, _camera, _random, _spawnSettings);
            }
            catch
            {
                UnityEngine.Object.Destroy(instance);
                throw;
            }
        }
    }
}