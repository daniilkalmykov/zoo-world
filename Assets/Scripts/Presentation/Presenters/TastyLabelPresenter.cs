using System;
using Cysharp.Threading.Tasks;
using Roots;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Presentation.Presenters
{
    internal sealed class TastyLabelPresenter
    {
        private const float Duration = 0.8f;
        private const float ScreenOffset = 32f;
        
        private readonly GameUIRoot _ui;
        private readonly Camera _camera;
        
        public TastyLabelPresenter(GameUIRoot ui, GameWorldRoot worldRoot)
        {
            _ui = ui;
            _camera = worldRoot.Camera;
        }
        
        public void Show(Transform target)
        {
            var label = Object.Instantiate(_ui.TastyLabelPrefab, _ui.FloatingLabelsRoot);

            label.text = "Tasty!";

            TrackAsync(label, target).Forget();
        }
        
        private async UniTaskVoid TrackAsync(TMP_Text label, Transform target)
        {
            var token = label.GetCancellationTokenOnDestroy();
            var elapsed = 0f;

            try
            {
                while (elapsed < Duration && target != null)
                {
                    UpdatePosition(label, target.position);

                    await UniTask.Yield(
                        PlayerLoopTiming.Update,
                        token);

                    elapsed += Time.unscaledDeltaTime;
                }
            }
            catch (OperationCanceledException)
                when (token.IsCancellationRequested)
            {
                // UI object was destroyed.
            }
            finally
            {
                if (label != null)
                    Object.Destroy(label.gameObject);
            }
        }
        
        private void UpdatePosition(TMP_Text label, Vector3 worldPosition)
        {
            var screenPosition =
                _camera.WorldToScreenPoint(worldPosition);

            if (screenPosition.z <= 0f)
                return;

            screenPosition.y -= ScreenOffset;

            var canvas = _ui.Canvas;

            var uiCamera =
                canvas.renderMode == RenderMode.ScreenSpaceOverlay
                    ? null
                    : canvas.worldCamera;

            if (RectTransformUtility
                .ScreenPointToLocalPointInRectangle(
                    _ui.FloatingLabelsRoot,
                    screenPosition,
                    uiCamera,
                    out var localPosition))
            {
                label.rectTransform.anchoredPosition =
                    localPosition;
            }
        }
    }
}