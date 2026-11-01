using Infrastructure.Movement;
using UnityEngine;

namespace Repositories.Movement
{
    [CreateAssetMenu(menuName = "Zoo World/Movement/Jump")]
    public sealed class JumpMovementConfig : MovementConfig
    {
        [Header("Jump")] [SerializeField, Min(0.1f)]
        private float _jumpInterval = 1.5f;

        [SerializeField, Min(0.1f)] private float _jumpDistance = 2f;

        [SerializeField, Min(0.1f)] private float _flightDuration = 0.6f;

        [Header("Ground Check")] [SerializeField]
        private LayerMask _groundMask;

        [SerializeField, Min(0.1f)] private float _groundCheckDistance = 0.65f;

        public override IMovementBehaviour Create(Rigidbody rigidbody)
        {
            return new JumpMovement(
                rigidbody,
                _jumpInterval,
                _jumpDistance,
                _flightDuration,
                _groundMask,
                _groundCheckDistance);
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            _jumpInterval = Mathf.Max(_jumpInterval, _flightDuration);
        }
    }
}