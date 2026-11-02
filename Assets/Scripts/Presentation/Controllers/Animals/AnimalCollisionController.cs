using System;
using System.Runtime.CompilerServices;
using Presentation.Views.Animals;
using Presentation.Views.Decorator;
using UnityEngine;

[assembly: InternalsVisibleTo("Infrastructure.Factories")]
namespace Presentation.Controllers.Animals
{
    internal sealed class AnimalCollisionController : IAnimalCollisionController, IDisposable
    {
        private readonly AnimalView _view;
        private readonly IColliderDecoratorView _colliderDecoratorView;

        public AnimalCollisionController(AnimalView view, IColliderDecoratorView colliderDecoratorView)
        {
            _view = view;
            _colliderDecoratorView = colliderDecoratorView;
            
            _colliderDecoratorView.CollisionEntered += OnCollisionEnter;
        }

        public event Action<AnimalView, AnimalView> Collided;
        
        public void Dispose()
        {
            if (_colliderDecoratorView != null) 
                _colliderDecoratorView.CollisionEntered -= OnCollisionEnter;
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (_view == null || _view.Model == null)
            {
                return;
            }

            AnimalView otherView = null;

            if (collision.rigidbody != null)
            {
                otherView = collision.rigidbody.GetComponent<AnimalView>();
            }

            if (otherView == null)
            {
                otherView = collision.collider.GetComponentInParent<AnimalView>();
            }

            if (otherView == null ||
                otherView == _view ||
                otherView.Model == null)
            {
                return;
            }

            Collided?.Invoke(_view, otherView);
        }

    }
}