using System;
using Domain.Interfaces;
using Domain.Models.Animals;
using Domain.Models.Interactions;

namespace UseCases.Animals
{
    internal sealed class ResolveAnimalsCollisionUseCase : IResolveAnimalsCollisionUseCase
    {
        private readonly IDeathList<AnimalType> _statistics;

        public ResolveAnimalsCollisionUseCase(IDeathList<AnimalType> statistics)
        {
            _statistics = statistics;
        }

        public AnimalCollisionResult Execute(IAnimal first, IAnimal second)
        {
            if (first == null)
                throw new ArgumentNullException(nameof(first));

            if (second == null)
                throw new ArgumentNullException(nameof(second));

            if ((first is IMortal firstMortal && second is IMortal secondMortal) == false ||
                ReferenceEquals(first, second) ||
                first.InstanceId == second.InstanceId ||
                firstMortal.IsAlive == false ||
                secondMortal.IsAlive == false)
            {
                return AnimalCollisionResult.None;
            }

            var victim = AnimalInteractionPolicy.SelectVictim(first, second);

            if (victim is not IMortal victimMortal || victimMortal.IsAlive == false)
                return AnimalCollisionResult.None;

            var winner = ReferenceEquals(victim, first)
                ? second
                : first;

            if (victimMortal.TryDie() == false)
                return AnimalCollisionResult.None;

            _statistics.Increase(victim.Type);

            return new AnimalCollisionResult(
                winner,
                victim);
        }
    }
}