using System;
using System.Runtime.CompilerServices;
using Infrastructure.Random;
using Repositories.Animals;
using Roots;
using UnityEngine;

[assembly: InternalsVisibleTo("Assembly-CSharp")]
namespace Infrastructure.Spawning
{
    internal sealed class CameraSpawningPositionProvider : ISpawningPositionProvider
    {
        private readonly Camera _camera;
        private readonly IAnimalsFactoryConfig _settings;
        private readonly IRandomSource _random;

        public CameraSpawningPositionProvider(GameWorldRoot world, IAnimalsFactoryConfig settings,
            IRandomSource random)
        {
            _camera = world.Camera;
            _settings = settings;
            _random = random;
        }

        public Vector3 GetRandomPosition()
        {
            var padding = _settings.ViewportPadding;

            var x = _random.Range(padding, 1f - padding);
            var y = _random.Range(padding, 1f - padding);

            var ray = _camera.ViewportPointToRay(new Vector3(x, y, 0f));

            var plane = new Plane(Vector3.up, new Vector3(0f, _settings.SpawnHeight, 0f));

            if (plane.Raycast(ray, out var distance) == false || distance < 0f)
            {
                throw new InvalidOperationException("Camera ray does not intersect the spawn plane.");
            }

            return ray.GetPoint(distance);
        }
    }
}