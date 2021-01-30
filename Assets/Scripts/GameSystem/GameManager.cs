using UniRx;
using UnityEngine;

namespace GameSystem
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float startingSanity;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            SetupPlayingLogic();
        }

        private void SetupPlayingLogic()
        {
            GameStateManager.CurrentGameState
                .Where(state => state == GameState.Playing)
                .Subscribe(_ => PlayerStats.SanityUpdater.Value = startingSanity)
                .AddTo(this);

            GameEvents.PlayerDeath
                .IsPlaying()
                .Subscribe(_ => RestartGame())
                .AddTo(this);

            GameEvents.SanityLowered
                .IsPlaying()
                .Subscribe(dto => PlayerStats.SanityUpdater.Value -= dto.amount)
                .AddTo(this);

            GameEvents.SanityGained
                .IsPlaying()
                .Subscribe(dto => PlayerStats.SanityUpdater.Value += dto.amount)
                .AddTo(this);

            PlayerStats.CurrentSanity
                .IsPlaying()
                .Where(amount => amount <= 0f)
                .Subscribe(dto => PlayerLostAllSanity())
                .AddTo(this);
        }

        private void RestartGame()
        {
            
        }

        private void PlayerLostAllSanity()
        {
            
        }
    }
}