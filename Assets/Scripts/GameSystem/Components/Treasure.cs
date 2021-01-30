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

        [SerializeField] private float speed = 0.1f;

        private void Update()
        {
            CalculateSanityDamage();

            MoveToPlayer();
        }

        private void MoveToPlayer()
        {
            var playerPosition = GameStateManager.CurrentPlayer.transform.position;
            var position = transform.position;
            var target = new Vector3(playerPosition.x, playerPosition.y, position.z);
            transform.position = Vector3.MoveTowards(position, target, speed * Time.deltaTime);
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