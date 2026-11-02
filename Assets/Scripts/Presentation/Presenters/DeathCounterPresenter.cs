using System;
using Domain.Interfaces;
using Domain.Models.Animals;
using Presentation.Views;
using Roots;
using VContainer.Unity;

namespace Presentation.Presenters
{
    public sealed class DeathCounterPresenter : IStartable, IDisposable
    { 
        private readonly IDeathList<AnimalType> _statistics;
        private readonly IDeathCounterView _view;

        public DeathCounterPresenter(IDeathList<AnimalType> statistics, GameUIRoot uiRoot)
        {
            _statistics = statistics;
            _view = uiRoot.DeathCounterView;
        }

        public void Start()
        {
            _statistics.Changed += Refresh;
            
            Refresh(AnimalType.Prey, 0);
            Refresh(AnimalType.Predator, 0);
        }

        public void Dispose()
        {
            _statistics.Changed -= Refresh;
        }
        
        private void Refresh(AnimalType type, uint value)
        {
            _view.SetCount(type, value);
        }

        
    }
}