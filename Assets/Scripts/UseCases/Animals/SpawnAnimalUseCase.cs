using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure.Factories.Animals;
using Infrastructure.Random;
using Infrastructure.Spawning;
using Repositories.Animals;
using UnityEngine;
using VContainer.Unity;

[assembly: InternalsVisibleTo("Assembly-CSharp")]
namespace UseCases.Animals
{
    internal sealed class SpawnAnimalUseCase : IAsyncStartable
    {
        private readonly IAnimalsCatalog _catalog;
        private readonly IAnimalFactory _factory;
        private readonly ISpawningPositionProvider _positionProvider;
        private readonly IRandomSource _random;
        private readonly IAnimalsFactoryConfig _settings;

        public SpawnAnimalUseCase(
            IAnimalsCatalog catalog,
            IAnimalFactory factory,
            ISpawningPositionProvider positionProvider,
            IRandomSource random, IAnimalsFactoryConfig settings)
        {
            _catalog = catalog;
            _factory = factory;
            _positionProvider = positionProvider;
            _random = random;
            _settings = settings;
        }

        public async UniTask StartAsync(CancellationToken cancellationToken = new())
        {
            try
            {
                while (cancellationToken.IsCancellationRequested == false)
                {
                    var delay = _random.Range(_settings.MinIntervalSeconds, _settings.MaxIntervalSeconds);

                    await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: cancellationToken);

                    cancellationToken.ThrowIfCancellationRequested();

                    Execute();
                }
            }
            catch (OperationCanceledException)
                when (cancellationToken.IsCancellationRequested)
            {
                // Expected on game scope disposal.
            }
        }
        
        private void Execute()
        {
            var animalIds = _catalog.AnimalRepositories.Select(repo => repo.Id).ToList();

            if (animalIds.Count == 0)
            {
                throw new InvalidOperationException("Animal catalog is empty.");
            }

            var index = _random.Range(0, animalIds.Count);
            var animalId = animalIds[index];

            var position = _positionProvider.GetRandomPosition();

            _factory.Create(animalId, position);
        }
    }
}