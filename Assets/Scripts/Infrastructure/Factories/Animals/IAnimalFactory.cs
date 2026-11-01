using Domain.Animals;
using UnityEngine;

namespace Infrastructure.Factories.Animals
{
    public interface IAnimalFactory
    {
        void Create(AnimalId id, Vector3 position);
    }
}