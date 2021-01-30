using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem.Components
{
    public class GameManagerComponent : MonoBehaviour
    {
        [SerializeField] private float startingSanity;
        [SerializeField] private string gameSceneName;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            SetupPlayingLogic();
        }

        private void SetupPlayingLogic()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;

            GameStateManager.CurrentGameState
                .Subscribe(state => Debug.Log($"Game state = {state.ToString()}"));
            
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
                .Subscribe(_ => GameScene.RestartGame())
                .AddTo(this);
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.IsPlayScene(gameSceneName))
            {
                GameStateManager.CurrentGameState.Value = GameState.Playing;
            }
        }
    }
}