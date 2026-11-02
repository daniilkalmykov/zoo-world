using Domain.Interfaces;
using Domain.Models.Animals;

namespace Domain.Models.Interactions
{
    public static class AnimalInteractionPolicy
    {
        public static IAnimal SelectVictim(IAnimal first, IAnimal second)
        {
            if (first.Type == AnimalType.Prey && second.Type == AnimalType.Prey)
            {
                return null;
            }

            if (first.Type != second.Type)
            {
                return first.Type == AnimalType.Prey ? first : second;
            }

            return first.InstanceId.CompareTo(second.InstanceId) < 0
                ? second
                : first;
        }
    }
}