using System;
using GameSystem.Dto;
using UniRx;
using UnityEngine;

namespace GameSystem.Components
{
    public abstract class SanityInterval : MonoBehaviour
    {
        public float amount;
        public long intervalMs;

        private void Start()
        {
            Observable.Timer(TimeSpan.FromMilliseconds(intervalMs))
                .Where(_ => isActiveAndEnabled)
                .Subscribe(_ => TriggerSanity())
                .AddTo(this);
        }

        protected abstract void TriggerSanity();
        
        protected SanityDto SanityAmount => new SanityDto
        {
            amount = amount
        };
    }
}