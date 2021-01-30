using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.sceneLoaded += OnSceneLoaded;
            
            GameStateManager.CurrentGameState
                .Where(state => state == GameState.Playing)
                .Subscribe(_ => PlayerStats.SanityUpdater.Value = startingSanity)
                .AddTo(this);

            GameEvents.SanityLowered
                .IsPlaying()
                .Subscribe(dto => PlayerStats.SanityUpdater.Value -= dto.amount)
                .AddTo(this);

            GameEvents.SanityGained
                .IsPlaying()
                .Subscribe(dto => PlayerStats.SanityUpdater.Value += dto.amount)
                .AddTo(this);

            GameStateManager.PlayerDiedAnimationCompleted
                .IsPlaying()
                .Subscribe(_ => RestartGame())
                .AddTo(this);
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private static void RestartGame()
        {
            GameStateManager.CurrentGameState.Value = GameState.Loading;
            ReloadGameScene();
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.IsPlayScene())
            {
                GameStateManager.CurrentGameState.Value = GameState.Playing;
            }
        }

        private static void ReloadGameScene()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
    }
}