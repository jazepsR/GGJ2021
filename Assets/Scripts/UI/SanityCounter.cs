using System;
using GameSystem;
using UniRx;
using UnityEngine;

namespace UI
{
    public class SanityCounter : MonoBehaviour
    {
        [SerializeField] private RectTransform bar;
        [SerializeField] private RectTransform whiteBar;
        [SerializeField] private float whiteBarDelayMs = 500;

        private IDisposable _disposable;
        private Action _setBar;
        private bool _previousState;
        
        private void Awake()
        {
            PlayerStats.CurrentSanity
                .Pairwise((previous, next) => (previous, next))
                .Subscribe(pair => SetSanityBar(pair.previous, pair.next))
                .AddTo(this);
        }

        private void SetSanityBar(float previous, float currentSanity)
        {
            var lostSanity = previous > currentSanity;
            
            if (PlayerStats.MaxSanity == 0f)
            {
                return;
            }

            if (_previousState != lostSanity && !lostSanity)
            {
                _setBar?.Invoke();
                _previousState = false;
            }
            
            _disposable?.Dispose();

            SetBar(currentSanity, bar);
            SetBar(currentSanity, whiteBar, lostSanity);
        }

        private void SetBar(float currentSanity, RectTransform rectTransform, bool withDelay)
        {
            if (withDelay)
            {
                _setBar = () => SetBar(currentSanity, rectTransform);
                _disposable = Observable.Return(Unit.Default).Delay(TimeSpan.FromMilliseconds(whiteBarDelayMs))
                    .Subscribe(_ =>
                    {
                        _setBar?.Invoke();
                        _disposable?.Dispose();
                        _setBar = null;
                        _disposable = null;
                    });
            }
            else
            {
                SetBar(currentSanity, rectTransform);
            }
        }

        private static void SetBar(float currentSanity, RectTransform rectTransform)
        {
            rectTransform.anchorMax = new Vector2(currentSanity / PlayerStats.MaxSanity, rectTransform.anchorMax.y);
        }
    }
}
