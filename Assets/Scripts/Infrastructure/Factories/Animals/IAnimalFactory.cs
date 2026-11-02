using System;
using Domain.Models.Animals;
using Presentation.Controllers.Animals;
using UnityEngine;

namespace Infrastructure.Factories.Animals
{
    public interface IAnimalFactory
    {
        event Action<IAnimalCollisionController> ControllerCreated;
        void Create(AnimalId id, Vector3 position);
    }
}