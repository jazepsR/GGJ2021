using System;
using System.Runtime.CompilerServices;
using UniRx;
using UnityEngine;

namespace GameSystem.Components
{
    public class Treasure : LoseSanityInterval
    {
        [SerializeField] private float maxLoss;
        [SerializeField] private float minLoss;
        [SerializeField] private long minIntervalMs;
        [SerializeField] private long maxIntervalMs;
        [SerializeField] private float maxDistance = 1;

        private void Start()
        {
            PlayerInCollider.Subscribe(value => Debug.Log($"Treasure collides with player {value}")).AddTo(this);
        }

        protected override void PreTriggerSanity()
        {
            if (!PlayerInCollider.Value)
            {
                return;
            }

            if (maxDistance < 1f)
            {
                maxDistance = 1;
            }
            
            
            var distance = Vector3.Distance(transform.position, GameStateManager.CurrentPlayer.transform.position) / maxDistance;
            if (distance > maxDistance)
                Debug.Log($"Distance is greater than max {distance} for max {maxDistance}");
            
            amount = maxLoss - Mathf.Lerp(0f, maxLoss - minLoss, distance);

            intervalMs = (long) Mathf.Lerp(minIntervalMs, maxIntervalMs,distance);
        }

        protected override void TriggerSanity()
        {
            GameEvents.LowerSanity(SanityAmount);
        }
    }
}