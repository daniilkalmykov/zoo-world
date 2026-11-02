using System;
using System.Runtime.CompilerServices;
using Domain.Models.Interactions;
using Infrastructure.Factories.Animals;
using Presentation.Controllers.Animals;
using Presentation.Views.Animals;
using UseCases.Animals;
using VContainer.Unity;

[assembly: InternalsVisibleTo("Assembly-CSharp")]
namespace Presentation.Presenters
{
    internal sealed class AnimalCollisionPresenter : IDisposable, IStartable
    {
        private readonly TastyLabelPresenter _tastyPresenter;
        private readonly IResolveAnimalsCollisionUseCase _resolveAnimalsCollisionUseCase;
        private readonly IAnimalFactory _animalsFactory;

        public AnimalCollisionPresenter(IAnimalFactory animalsFactory,
            IResolveAnimalsCollisionUseCase resolveAnimalsCollisionUseCase, TastyLabelPresenter tastyPresenter)
        {
            _animalsFactory = animalsFactory;
            _resolveAnimalsCollisionUseCase = resolveAnimalsCollisionUseCase;
            _tastyPresenter = tastyPresenter;
        }
        
        public void Start()
        {
            _animalsFactory.ControllerCreated += OnControllerCreated;
        }
        
        public void Dispose()
        {
            _animalsFactory.ControllerCreated -= OnControllerCreated;
        }

        private void OnControllerCreated(IAnimalCollisionController obj)
        {
            obj.Collided += OnCollided;
        }

        private void OnCollided(AnimalView first, AnimalView second)
        {
            var result = _resolveAnimalsCollisionUseCase.Execute(first.Model, second.Model);

            Present(result, first, second);
        }

        private void Present(AnimalCollisionResult result, AnimalView first, AnimalView second)
        {
            if (result.HasKill == false)
                return;

            var victimView = ReferenceEquals(first.Model, result.Victim) ? first : second;
            var winnerView = ReferenceEquals(first.Model, result.Winner) ? first : second;

            _tastyPresenter.Show(winnerView.transform);

            victimView.Die();
        }

    }
}