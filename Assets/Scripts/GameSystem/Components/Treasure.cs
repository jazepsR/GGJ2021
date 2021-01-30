using System;
using UniRx;
using UnityEngine;

namespace GameSystem.Components
{
    public class Treasure : MonoBehaviour
    {
        [SerializeField] private LoseSanityInterval interval;
        
        [SerializeField] private float maxLoss;
        [SerializeField] private float minLoss;
        [SerializeField] private long minIntervalMs;
        [SerializeField] private long maxIntervalMs;
        [SerializeField] private float maxDistance = 1;

        [SerializeField] private float moveToPlayerSpeed = 2f;

        [SerializeField] private float startUpSpeed = 3;
        [SerializeField] private float zPositionEnd = 4;
        [SerializeField] private float yPositionEnd = 7;

        [SerializeField] private double delayToStart = 2;

        private bool _finishedStartup;
        private Vector3 _targetStartPosition;
        private bool _shouldFollow;

        private void Start()
        {
            Transform transform1;
            (transform1 = transform).Rotate(90, 0, 0);
            var position = transform1.position;
            _targetStartPosition = position + (Vector3.back * zPositionEnd) + (Vector3.up * yPositionEnd);
        }

        private void Update()
        {
            CalculateSanityDamage();
            
            if (_finishedStartup)
            {
                if (_shouldFollow)
                {
                    MoveToPlayer();
                }
            }
            else
            {
                interval.shouldLowerSanity = false;
                var position = transform.position;
                transform.position = Vector3.MoveTowards(position, _targetStartPosition,
                    Time.deltaTime * startUpSpeed);
                
                if (!(Vector3.Distance(transform.position, _targetStartPosition) <= 0.00001f)) return;
                _finishedStartup = true;
                Observable.Return(Unit.Default).Delay(TimeSpan.FromSeconds(delayToStart))
                    .Take(1)
                    .Subscribe(_ =>
                    {
                        interval.shouldLowerSanity = true;
                        _shouldFollow = true;
                    }).AddTo(this);
            }
        }

        private void MoveToPlayer()
        {
            var playerPosition = GameStateManager.CurrentPlayer.transform.position;
            var position = transform.position;
            var target = new Vector3(playerPosition.x, playerPosition.y, position.z);
            transform.position = Vector3.MoveTowards(position, target, moveToPlayerSpeed * Time.deltaTime);
        }

        private void CalculateSanityDamage()
        {
            if (!interval.PlayerInCollider.Value)
            {
                return;
            }

            if (maxDistance < 1f)
            {
                maxDistance = 1;
            }


            var distance = Vector3.Distance(transform.position, GameStateManager.CurrentPlayer.transform.position) /
                           maxDistance;
            if (distance > maxDistance)
                Debug.Log($"Distance is greater than max {distance} for max {maxDistance}");

            interval.amount = maxLoss - Mathf.Lerp(0f, maxLoss - minLoss, distance);

            interval.intervalMs = (long) Mathf.Lerp(minIntervalMs, maxIntervalMs, distance);
        }
    }
}