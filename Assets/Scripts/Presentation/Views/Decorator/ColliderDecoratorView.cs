using System;
using UnityEngine;

namespace Presentation.Views.Decorator
{
    internal sealed class ColliderDecoratorView : MonoBehaviour, IColliderDecoratorView
    {
        public event Action<Collision> CollisionEntered;

        private void OnCollisionEnter(Collision other)
        {
            CollisionEntered?.Invoke(other);
        }
    }
}