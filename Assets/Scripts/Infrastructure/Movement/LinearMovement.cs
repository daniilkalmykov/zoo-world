using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("Repositories")]
namespace Infrastructure.Movement
{
    internal sealed class LinearMovement : IMovementBehaviour
    {
        private readonly Rigidbody _body;
        private readonly float _speed;
        private readonly float _acceleration;

        public LinearMovement(Rigidbody body, float speed, float acceleration)
        {
            _body = body;
            _speed = speed;
            _acceleration = acceleration;
        }

        public void FixedTick(Vector3 direction, float deltaTime)
        {
            if (direction.sqrMagnitude < 0.0001f)
                return;

            var targetVelocity = direction.normalized * _speed;

            var currentVelocity = _body.linearVelocity;
            currentVelocity.y = 0f;

            var deltaVelocity = targetVelocity - currentVelocity;
            var maxVelocityChange = _acceleration * deltaTime;

            var velocityChange = Vector3.ClampMagnitude(deltaVelocity, maxVelocityChange);

            _body.AddForce(velocityChange, ForceMode.VelocityChange);
        }
    }
}