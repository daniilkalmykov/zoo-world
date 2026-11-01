using System;
using Infrastructure.Directions;
using Infrastructure.Movement;
using Infrastructure.Random;
using Repositories.Animals;
using Repositories.Movement;
using UnityEngine;

namespace Presentation.Controllers.Animals
{
    public sealed class AnimalMovementController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        private IMovementBehaviour _movement;
        private RandomWanderDirection _wander;
        private ScreenBoundsDirection _screenBounds;

        private bool _initialized;

        public void Initialize(MovementConfig config, Camera camera, IRandomSource random,
            IAnimalsFactoryConfig spawnSettings)
        {
            if (_initialized)
                throw new InvalidOperationException("Movement is already initialized.");

            if (config == null)
                throw new ArgumentNullException(nameof(config));

            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody>();

            if (_rigidbody.isKinematic)
                throw new InvalidOperationException("Movement requires a dynamic Rigidbody.");

            _movement = config.Create(_rigidbody);

            _wander = new RandomWanderDirection(
                random,
                config.MinDirectionChangeInterval,
                config.MaxDirectionChangeInterval);

            _screenBounds = new ScreenBoundsDirection(
                camera,
                spawnSettings.ViewportPadding,
                spawnSettings.SpawnHeight);

            _initialized = true;
        }

        private void FixedUpdate()
        {
            if (_initialized == false)
                return;

            var deltaTime = Time.fixedDeltaTime;

            _wander.Tick(deltaTime);

            var direction = _screenBounds.Resolve(_rigidbody.position, _wander.Direction);

            _movement.FixedTick(direction, deltaTime);
        }
    }
}