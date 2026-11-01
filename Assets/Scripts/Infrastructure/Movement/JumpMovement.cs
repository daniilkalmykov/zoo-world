using System;
using UnityEngine;

namespace Infrastructure.Movement
{
    internal sealed class JumpMovement : IMovementBehaviour
    {
        private readonly Rigidbody _body;
        private readonly float _jumpInterval;
        private readonly float _jumpDistance;
        private readonly float _flightDuration;
        private readonly LayerMask _groundMask;
        private readonly float _groundCheckDistance;

        private float _elapsed;

        public JumpMovement(Rigidbody body, float jumpInterval, float jumpDistance, float flightDuration,
            LayerMask groundMask, float groundCheckDistance)
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if (body.useGravity == false)
                throw new ArgumentException("JumpMovement requires gravity.");

            if (Physics.gravity.y >= -0.01f)
                throw new InvalidOperationException("JumpMovement requires downward gravity.");

            _body = body;
            _jumpInterval = jumpInterval;
            _jumpDistance = jumpDistance;
            _flightDuration = flightDuration;
            _groundMask = groundMask;
            _groundCheckDistance = groundCheckDistance;
        }

        public void FixedTick(Vector3 direction, float deltaTime)
        {
            _elapsed += deltaTime;

            if (_elapsed < _jumpInterval)
                return;

            if (IsGrounded() == false)
                return;

            if (direction.sqrMagnitude < 0.0001f)
                return;

            Jump(direction.normalized);

            _elapsed = 0f;
        }

        private bool IsGrounded()
        {
            return Physics.Raycast(
                _body.position,
                Vector3.down,
                _groundCheckDistance,
                _groundMask,
                QueryTriggerInteraction.Ignore);
        }

        private void Jump(Vector3 direction)
        {
            var horizontalSpeed = _jumpDistance / _flightDuration;
            var verticalSpeed = -Physics.gravity.y * _flightDuration * 0.5f;

            var targetVelocity = direction * horizontalSpeed + Vector3.up * verticalSpeed;
            
            var currentVelocity = _body.linearVelocity;

            var velocityChange = targetVelocity - currentVelocity;

            _body.AddForce(velocityChange, ForceMode.VelocityChange);
        }
    }
}