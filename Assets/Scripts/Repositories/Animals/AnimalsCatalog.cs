using System.Collections.Generic;
using UnityEngine;

namespace Repositories.Animals
{
    [CreateAssetMenu(fileName = "AnimalsCatalog", menuName = "Zoo World/Animals/Animals Catalog")]
    internal sealed class AnimalsCatalog : ScriptableObject, IAnimalsCatalog
    {
        [SerializeField] private AnimalRepository[] _animalRepositories;

        public IReadOnlyList<IAnimalRepository> AnimalRepositories => _animalRepositories;
    }
}