using Infrastructure.Movement;
using UnityEngine;

namespace Repositories.Movement
{
    public abstract class MovementConfig : ScriptableObject
    {
        [Header("Direction")]
        [SerializeField, Min(0.1f)]
        private float _minDirectionChangeInterval = 1f;

        [SerializeField, Min(0.1f)]
        private float _maxDirectionChangeInterval = 3f;

        public float MinDirectionChangeInterval => _minDirectionChangeInterval;

        public float MaxDirectionChangeInterval => _maxDirectionChangeInterval;

        public abstract IMovementBehaviour Create(Rigidbody rigidbody);

        protected virtual void OnValidate()
        {
            _maxDirectionChangeInterval = Mathf.Max(_minDirectionChangeInterval, _maxDirectionChangeInterval);
        }
    }
}