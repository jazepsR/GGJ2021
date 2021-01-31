using System;
using GameSystem.Dto;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace GameSystem.Components.Core
{
    public class GameManagerComponent : MonoBehaviour
    {
        [FormerlySerializedAs("startingSanity")] [SerializeField] private float maxSanity;
        [SerializeField] private string gameSceneName;

        private void Awake()
        {
            PlayerStats.MaxSanity = maxSanity;
            DontDestroyOnLoad(this);
            SetupPlayingLogic();

            GameStateManager.TreasureSpawned
                .Subscribe(value => Debug.Log($"Treasure spawned {value}"))
                .AddTo(this);
        }

        private void SetupPlayingLogic()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;

            GameStateManager.CurrentGameState
                .Subscribe(state => Debug.Log($"Game state = {state.ToString()}"));
            
            GameStateManager.CurrentGameState
                .Where(state => state == GameState.Playing)
                .Subscribe(_ => PlayerStats.SanityUpdater.Value = maxSanity)
                .AddTo(this);

            GameEvents.SanityLowered
                .IsPlaying()
                .Subscribe(dto => PlayerStats.SanityUpdater.Value -= dto.amount)
                .AddTo(this);

            PlayerStats.CurrentSanity
                .IsPlaying()
                .Where(value => value <= 0)
                .Subscribe(_ => GameEvents.KillPlayer(new PlayerDeathDto()))
                .AddTo(this);

            GameEvents.SanityGained
                .IsPlaying()
                .Subscribe(dto => PlayerStats.SanityUpdater.Value = Math.Min(maxSanity, PlayerStats.CurrentSanity.Value + dto.amount))
                .AddTo(this);

            GameStateManager.CurrentGameState
                .Where(state => state == GameState.Menu)
                .Subscribe(_ => Pause())
                .AddTo(this);

            GameStateManager.CurrentGameState
                .Where(state => state == GameState.Playing)
                .Subscribe(_ => UnPause())
                .AddTo(this);
        }

        private void Pause()
        {
            Time.timeScale = 0;
        }
        private void UnPause()
        {
            Time.timeScale = 1f;
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