using System;
using Domain.Interfaces;
using Domain.Models.Animals;
using Infrastructure.Directions;
using Infrastructure.Movement;
using UnityEngine;

namespace Presentation.Views.Animals
{
    public abstract class AnimalView : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        
        public IAnimal Model { get; private set; }
        
        private IMovementBehaviour _movement;
        private RandomWanderDirection _wander;
        private ScreenBoundsDirection _screenBounds;
        
        public AnimalId AnimalId => Model.Id;
        public AnimalType Type => Model.Type;

        public void Initialize(IAnimal animal)
        {
            if (Model != null)
                throw new InvalidOperationException("Animal is already initialized.");

            Model = animal ?? throw new ArgumentNullException(nameof(animal));
        }
        
        public void Die()
        {
            _collider.enabled = false;

            gameObject.SetActive(false);

            Destroy(gameObject);
        }
    }
}
