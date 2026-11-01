using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Domain.Animals;
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
        private readonly IAnimalsCatalog _animalsCatalog;
        private readonly GameWorldRoot _world;

        public AnimalFactory(IObjectResolver resolver, IAnimalsCatalog animalsCatalog, GameWorldRoot world)
        {
            _resolver = resolver;
            _animalsCatalog = animalsCatalog;
            _world = world;
        }

        public void Create(AnimalId id, Vector3 position)
        {
            var config =
                _animalsCatalog.AnimalRepositories.FirstOrDefault(config => config.Id == id);

            if (config == null)
            {
                throw new InvalidOperationException($"Animal with id {id} not found in catalog.");
            }
            
            var instance = _resolver.Instantiate(
                config.Prefab,
                position,
                Quaternion.identity,
                _world.AnimalsRoot);

            var view = instance.GetComponent<AnimalView>();

            view.Initialize(config.Id, config.Type);
        }
    }
}