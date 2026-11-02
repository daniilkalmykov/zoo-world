using System;
using Domain.Models.Animals;

namespace Domain.Interfaces
{
    public interface IAnimal
    {
        AnimalType Type { get; }
        AnimalId Id { get; }
        Guid InstanceId { get; }
    }
}