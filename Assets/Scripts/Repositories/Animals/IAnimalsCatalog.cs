using System.Collections.Generic;

namespace Repositories.Animals
{
    public interface IAnimalsCatalog
    {
        IReadOnlyList<IAnimalRepository> AnimalRepositories { get; }
    }
}