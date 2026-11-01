using UnityEngine;

namespace Infrastructure.Directions
{
    public sealed class ScreenBoundsDirection
    {
        private readonly Camera _camera;
        private readonly float _padding;
        private readonly float _groundHeight;

        public ScreenBoundsDirection(Camera camera, float padding, float groundHeight)
        {
            _camera = camera;
            _padding = padding;
            _groundHeight = groundHeight;
        }

        public Vector3 Resolve(Vector3 worldPosition, Vector3 desiredDirection)
        {
            var viewport = _camera.WorldToViewportPoint(worldPosition);

            if (IsInsideBounds(viewport))
                return desiredDirection;

            return GetReturnDirection(worldPosition, desiredDirection);
        }

        private bool IsInsideBounds(Vector3 viewport)
        {
            return viewport.z > 0f &&
                   viewport.x >= _padding &&
                   viewport.x <= 1f - _padding &&
                   viewport.y >= _padding &&
                   viewport.y <= 1f - _padding;
        }

        private Vector3 GetReturnDirection(Vector3 worldPosition, Vector3 fallback)
        {
            var ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            var plane = new Plane(Vector3.up, new Vector3(0f, _groundHeight, 0f));

            if (plane.Raycast(ray, out var distance) == false)
                return fallback;

            var center = ray.GetPoint(distance);

            var direction = center - worldPosition;
            direction.y = 0f;

            return direction.sqrMagnitude < 0.0001f ? fallback : direction.normalized;
        }
    }
}