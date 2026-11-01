using System;
using Domain.Animals;
using Infrastructure.Directions;
using Infrastructure.Movement;
using UnityEngine;

namespace Presentation.Views.Animals
{
    public abstract class AnimalView : MonoBehaviour
    {
        private IMovementBehaviour _movement;
        private RandomWanderDirection _wander;
        private ScreenBoundsDirection _screenBounds;
        
        public AnimalId? AnimalId { get; private set; }
        public AnimalType? Type { get; private set; }

        public void Initialize(AnimalId animalId, AnimalType animalType)
        {
            if (AnimalId != null && Type != null)
                throw new InvalidOperationException("Animal is already initialized.");

            AnimalId = animalId;
            Type = animalType;
        }
    }
}