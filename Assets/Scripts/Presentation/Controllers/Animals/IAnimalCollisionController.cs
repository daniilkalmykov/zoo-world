using System;
using Presentation.Views.Animals;

namespace Presentation.Controllers.Animals
{
    public interface IAnimalCollisionController
    {
        event Action<AnimalView, AnimalView> Collided;
    }
}