using System;
using System.Runtime.CompilerServices;
using Domain.Interfaces;

[assembly: InternalsVisibleTo("Infrastructure.Factories")]
namespace Domain.Models.Animals
{
    internal sealed class Animal : IAnimal, IMortal
    {
        public Animal(AnimalType type, AnimalId id, Guid instanceId)
        {
            Type = type;
            Id = id;
            InstanceId = instanceId;
        }

        public AnimalType Type { get; }
        public AnimalId Id { get; }
        public Guid InstanceId { get; }

        public bool IsAlive { get; private set; } = true;
        
        public bool TryDie()
        {
            if (IsAlive == false)
                return false;

            IsAlive = false;
            
            return true;
        }
    }
}