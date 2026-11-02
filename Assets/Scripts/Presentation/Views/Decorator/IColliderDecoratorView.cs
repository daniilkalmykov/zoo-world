using System;
using UnityEngine;

namespace Presentation.Views.Decorator
{
    public interface IColliderDecoratorView
    {
        event Action<Collision> CollisionEntered;
    }
}