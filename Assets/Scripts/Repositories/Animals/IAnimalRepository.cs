using Domain.Animals;
using Presentation.Views.Animals;

namespace Repositories.Animals
{
    public interface IAnimalRepository
    {
        public AnimalId Id { get; }
        public AnimalType Type { get; }
        public AnimalView Prefab { get; }
    }
}