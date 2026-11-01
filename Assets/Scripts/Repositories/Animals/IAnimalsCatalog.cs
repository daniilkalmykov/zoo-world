using System.Collections.Generic;
using Domain.Animals;

namespace Repositories.Animals
{
    public interface IAnimalsCatalog
    {
        IReadOnlyList<IAnimalRepository> AnimalRepositories { get; }
    }
}