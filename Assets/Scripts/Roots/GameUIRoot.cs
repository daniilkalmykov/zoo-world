using System;
using UnityEngine;

namespace Roots
{
    public sealed class GameUIRoot : MonoBehaviour 
    {
        [SerializeField] private Canvas _canvas;

        public Canvas Canvas => _canvas;

        public void Initialize(Camera camera)
        {
            if (_canvas == null)
                throw new InvalidOperationException("UI Canvas is not assigned.");

            if (_canvas.renderMode != RenderMode.ScreenSpaceOverlay)
                _canvas.worldCamera = camera;
        }
    }
}