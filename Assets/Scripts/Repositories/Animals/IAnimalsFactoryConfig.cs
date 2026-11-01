namespace Repositories.Animals
{
    public interface IAnimalsFactoryConfig
    {
        public float MinIntervalSeconds { get; }
        public float MaxIntervalSeconds { get; }
        public float ViewportPadding { get; }
        public float SpawnHeight { get; }
    }
}