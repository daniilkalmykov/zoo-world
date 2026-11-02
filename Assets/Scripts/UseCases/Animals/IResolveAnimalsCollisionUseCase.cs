using Domain.Interfaces;
using Domain.Models.Interactions;

namespace UseCases.Animals
{
    public interface IResolveAnimalsCollisionUseCase
    {
        AnimalCollisionResult Execute(IAnimal first, IAnimal second);
    }
}