using UniRx;
using UnityEngine;

namespace GameSystem.Components.Core
{
    public class Shrine : MonoBehaviour
    {
        [SerializeField] private PlayerCollider playerCollider;

        private void Awake()
        {
            playerCollider.PlayerInCollider
                .IfTrue()
                .Where(_ => GameStateManager.TreasureSpawned.Value)
                .Subscribe(WinGame)
                .AddTo(this);

            GameStateManager.TreasureSpawned
                .Subscribe(ChangeState)
                .AddTo(this);
        }

        private static void WinGame(bool _)
        {
            GameStateManager.PlayerCompleteLevelSubject.OnNext(Unit.Default);
        }

        private void ChangeState(bool isSpawned)
        {
            
        }
    }
}