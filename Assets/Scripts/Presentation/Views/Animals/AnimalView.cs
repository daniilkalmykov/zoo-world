using System;
using Domain.Animals;
using UnityEngine;

namespace Presentation.Views.Animals
{
    public abstract class AnimalView : MonoBehaviour
    {
        public AnimalId? AnimalId { get; private set; }
        public AnimalType? Type { get; private set; }

        public void Initialize(AnimalId animalId, AnimalType animalType)
        {
            if (AnimalId != null && Type != null)
            {
                throw new InvalidOperationException("Animal is already initialized.");
            }

            AnimalId = animalId;
            Type = animalType;
        }
    }
}