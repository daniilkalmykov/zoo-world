using UnityEngine;

namespace Infrastructure.Spawning
{
    public interface ISpawningPositionProvider
    {
        Vector3 GetRandomPosition();
    }
}