using System;
using Presentation.Views;
using TMPro;
using UnityEngine;

namespace Roots
{
    public sealed class GameUIRoot : MonoBehaviour 
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private DeathCounterView _deathCounterView;
        [SerializeField] private RectTransform _floatingLabelsRoot;
        [SerializeField] private TMP_Text _tastyLabelPrefab;

        public RectTransform FloatingLabelsRoot => _floatingLabelsRoot;

        public TMP_Text TastyLabelPrefab => _tastyLabelPrefab;
        
        public Canvas Canvas => _canvas;
        public IDeathCounterView DeathCounterView => _deathCounterView;

        public void Initialize(Camera camera)
        {
            if (_canvas == null)
                throw new InvalidOperationException("UI Canvas is not assigned.");

            if (_canvas.renderMode != RenderMode.ScreenSpaceOverlay)
                _canvas.worldCamera = camera;
        }
    }
}