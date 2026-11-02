using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Domain.Interfaces;

[assembly: InternalsVisibleTo("Assembly-CSharp")]
namespace Domain.Models.Animals
{
    internal sealed class AnimalsDeathList : IDeathList<AnimalType>
    {
        private readonly Dictionary<AnimalType, uint> _deathCounts = new();

        public event Action<AnimalType, uint> Changed;

        public void InitializeTypes(AnimalType[] types)
        {
            foreach (var type in types)
            {
                _deathCounts.TryAdd(type, 0);
            }
        }

        public void Increase(AnimalType key)
        {
            if (_deathCounts.ContainsKey(key) == false) 
                return;
            
            _deathCounts[key] += 1;

            Changed?.Invoke(key, _deathCounts[key]);
        }
    }
}