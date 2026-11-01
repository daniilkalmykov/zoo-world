using UnityEngine;

namespace Infrastructure.Movement
{
    public interface IMovementBehaviour
    {
        void FixedTick(Vector3 direction, float deltaTime);
    }
}