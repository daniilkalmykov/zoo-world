using Domain.Models.Animals;
using Presentation.Views.Animals;
using Repositories.Movement;

namespace Repositories.Animals
{
    public interface IAnimalRepository
    {
        public AnimalId Id { get; }
        public AnimalType Type { get; }
        public AnimalView Prefab { get; }
        public MovementConfig MovementConfig { get; }
    }
}