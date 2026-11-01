namespace Infrastructure.Random
{
    public interface IRandomSource
    {
        public int Range(int minInclusive, int maxExclusive);

        public float Range(float minInclusive, float maxInclusive);
    }
}