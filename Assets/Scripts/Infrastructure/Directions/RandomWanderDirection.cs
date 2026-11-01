using Infrastructure.Random;
using UnityEngine;

namespace Infrastructure.Directions
{
    public sealed class RandomWanderDirection
    {
        private readonly IRandomSource _random;
        private readonly float _minInterval;
        private readonly float _maxInterval;

        private float _remainingTime;
        private Vector3 _direction;

        public Vector3 Direction => _direction;

        public RandomWanderDirection(IRandomSource random, float minInterval, float maxInterval)
        {
            _random = random;
            _minInterval = minInterval;
            _maxInterval = maxInterval;
        }

        public void Tick(float deltaTime)
        {
            _remainingTime -= deltaTime;

            if (_remainingTime > 0f)
                return;

            var angle = _random.Range(0f, Mathf.PI * 2f);

            _direction = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));

            _remainingTime = _random.Range(_minInterval, _maxInterval);
        }
    }
}