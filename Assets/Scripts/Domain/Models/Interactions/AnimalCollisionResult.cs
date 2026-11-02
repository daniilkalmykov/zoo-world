using Domain.Interfaces;

namespace Domain.Models.Interactions
{
    public readonly struct AnimalCollisionResult
    {
        public static AnimalCollisionResult None => default;

        public IAnimal Winner { get; }
        public IAnimal Victim { get; }

        public bool HasKill => Victim != null;

        public AnimalCollisionResult(IAnimal winner, IAnimal victim)
        {
            Winner = winner;
            Victim = victim;
        }
    }
}