using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Assembly-CSharp")]
namespace Infrastructure.Random
{
    internal sealed class UnityRandomSource : IRandomSource
    {
        public int Range(int minInclusive, int maxExclusive)
        {
            return UnityEngine.Random.Range(minInclusive, maxExclusive);
        }

        public float Range(float minInclusive, float maxInclusive)
        {
            return UnityEngine.Random.Range(minInclusive, maxInclusive);
        }
    }
}