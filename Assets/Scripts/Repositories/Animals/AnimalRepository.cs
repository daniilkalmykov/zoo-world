using Domain.Animals;
using Presentation.Views.Animals;
using UnityEngine;

namespace Repositories.Animals
{
    [CreateAssetMenu(menuName = "Zoo World/Animals/Animal Repository", fileName = "Animal Repository")]
    internal sealed class AnimalRepository : ScriptableObject, IAnimalRepository
    {
        [field: SerializeField] public AnimalId Id { get; private set; }
        [field: SerializeField] public AnimalType Type { get; private set; }
        [field: SerializeField] public AnimalView Prefab { get; private set; }
    }
}