using System;
using GameSystem.Dto;
using UniRx;
using UnityEngine;

namespace GameSystem.Components
{
    public class LoseSanity : MonoBehaviour
    {
        public float amount;
        public long intervalMs;

        private void Start()
        {
            Observable.Timer(TimeSpan.FromMilliseconds(intervalMs))
                .Where(_ => isActiveAndEnabled)
                .Subscribe(_ => GameEvents.LowerSanity(SanityAmount))
                .AddTo(this);
        }
        
        private SanityDto SanityAmount => new SanityDto
        {
            amount = amount
        };
    }
}