using System;
using GameSystem.Dto;
using UniRx;
using UnityEngine;

namespace GameSystem.Components
{
    public abstract class SanityInterval : PlayerCollider
    {
        private IDisposable _disposable;
        
        public float amount;
        public long intervalMs;

        private float _timePassed;

        private void Start()
        {
            PlayerInCollider.Subscribe(value => Debug.Log($"Player in {value}")).AddTo(this);
        }

        private void Update()
        {
            if (!PlayerInCollider.Value)
            {
                _timePassed = 0f;
                return;
            }

            _timePassed += Time.deltaTime * 1000;
            
            if (_timePassed < intervalMs)
            {
                return;
            }

            _timePassed = 0f;
            
            PreTriggerSanity();
            TriggerSanity();
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }

        protected virtual void PreTriggerSanity()
        {
            
        }

        protected abstract void TriggerSanity();
        
        protected SanityDto SanityAmount => new SanityDto
        {
            amount = amount
        };
    }
}