using System;
using Domain.Interfaces;
using UnityEngine;

namespace Presentation.Views.Animals
{
    public abstract class AnimalView : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        
        public IAnimal Model { get; private set; }
        
        public void Initialize(IAnimal animal)
        {
            if (Model != null)
                throw new InvalidOperationException("Animal is already initialized.");

            Model = animal ?? throw new ArgumentNullException(nameof(animal));
            _collider.enabled = true;
        }
        
        public void Die()
        {
            _collider.enabled = false;

            gameObject.SetActive(false);

            Destroy(gameObject);
        }
    }
}
