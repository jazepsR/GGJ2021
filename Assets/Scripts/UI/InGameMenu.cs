using GameSystem;
using Interactables;
using TMPro;
using UniRx;
using UnityEngine;

namespace UI
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private GameObject ui;

        private const string KGameOver = "Game Over";
        private const string KPaused = "Paused";
        private const string KWon = "Level Complete";

        private readonly SerialDisposable _disposable = new SerialDisposable();

        private void Awake()
        {
            GameStateManager.CurrentGameState
                .Where(state => state == GameState.Menu)
                .Subscribe(_ => ui.SetActive(true))
                .AddTo(this);
            
            GameStateManager.CurrentGameState
                .Where(state => state == GameState.Playing)
                .Subscribe(_ => ui.SetActive(false))
                .AddTo(this);
            
            KeyPress.MenuPressed
                .Where(_ => GameStateManager.CurrentGameState.Value == GameState.Playing)
                .Subscribe(_ => Pause())
                .AddTo(this);

            GameStateManager.PlayerDiedAnimationCompleted
                .IsPlaying()
                .Subscribe(_ => EndGame())
                .AddTo(this);

            GameStateManager.PlayerCompleteLevel
                .IsPlaying()
                .Subscribe(_ => WinGame())
                .AddTo(this);
        }

        private void EndGame()
        {
            text.text = KGameOver;
            EndGameKeyTrigger();
        }

        private void WinGame()
        {
            text.text = KWon;
            EndGameKeyTrigger();
        }

        private void Pause()
        {
            text.text = KPaused;
            Debug.Log("Pause");
            GameStateManager.CurrentGameState.Value = GameState.Menu;
                        
            _disposable.Disposable = KeyPress.AnyKeyPressed.Take(1)
                .Subscribe(_ => UnPause());
        }

        private void UnPause()
        {
            Debug.Log("Unpause");
            GameStateManager.CurrentGameState.Value = GameState.Playing;
            _disposable.Disposable.Dispose();
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }

        private void EndGameKeyTrigger()
        {
            GameStateManager.CurrentGameState.Value = GameState.Menu;

            Observable.Return(Unit.Default)
                .DelayFrameSubscription(5)
                .Take(1)
                .ContinueWith(KeyPress.AnyKeyPressed)
                .Subscribe(_ => GameScene.RestartGame())
                .AddTo(this);
        }
    }
}