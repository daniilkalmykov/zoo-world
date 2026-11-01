using UnityEngine;

namespace Repositories.Animals
{
    [CreateAssetMenu(menuName = "Zoo World/Animals/Spawn Settings")]
    internal sealed class AnimalsFactoryConfig : ScriptableObject, IAnimalsFactoryConfig
    {
        [Header("Spawn Interval")] [SerializeField, Min(0.1f)]
        private float _minIntervalSeconds = 1f;

        [SerializeField, Min(0.1f)]
        private float _maxIntervalSeconds = 2f;

        [Header("Spawn Area")]
        [SerializeField, Range(0f, 0.45f)]
        private float _viewportPadding = 0.1f;

        [SerializeField]
        private float _spawnHeight = 0.5f;

        public float MinIntervalSeconds => _minIntervalSeconds;
        public float MaxIntervalSeconds => _maxIntervalSeconds;
        public float ViewportPadding => _viewportPadding;
        public float SpawnHeight => _spawnHeight;

        private void OnValidate()
        {
            _maxIntervalSeconds = Mathf.Max(_minIntervalSeconds, _maxIntervalSeconds);
        }
    }
}