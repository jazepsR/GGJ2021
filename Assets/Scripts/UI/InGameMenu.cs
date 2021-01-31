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
            
            KeyPress.AnyKeyPressed
                .Where(_ => GameStateManager.CurrentGameState.Value == GameState.Menu)
                .Subscribe(_ => UnPause())
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
            GameStateManager.CurrentGameState.Value = GameState.Menu;
            
            KeyPress.AnyKeyPressed
                .Subscribe(_ => GameScene.RestartGame())
                .AddTo(this);
        }

        private void WinGame()
        {
            text.text = KWon;
            GameStateManager.CurrentGameState.Value = GameState.Menu;
            
            KeyPress.AnyKeyPressed
                .Subscribe(_ => GameScene.RestartGame())
                .AddTo(this);
        }

        private void Pause()
        {
            text.text = KPaused;
            GameStateManager.CurrentGameState.Value = GameState.Menu;
        }
        
        private void UnPause()
        {
            GameStateManager.CurrentGameState.Value = GameState.Playing;
        }
    }
}