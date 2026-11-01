using Infrastructure.Movement;
using UnityEngine;

namespace Repositories.Movement
{
    [CreateAssetMenu(menuName = "Zoo World/Movement/Linear")]
    internal sealed class LinearMovementConfig : MovementConfig
    {
        [Header("Linear Movement")]
        [SerializeField, Min(0.1f)]
        private float _speed = 2f;

        [SerializeField, Min(0.1f)]
        private float _acceleration = 30f;

        public override IMovementBehaviour Create(Rigidbody rigidbody)
        {
            return new LinearMovement(rigidbody, _speed, _acceleration);
        }
    }
}