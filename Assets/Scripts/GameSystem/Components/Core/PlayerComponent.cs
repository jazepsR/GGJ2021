using UniRx;
using UnityEngine;

namespace GameSystem.Components.Core
{
    public class PlayerComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource jump;
        [SerializeField] private AudioSource sprinting;
        
        private void Awake()
        {
            GameStateManager.currentPlayerSet = this;
            PlayerStats.IsJumping
                .IfTrue()
                .Subscribe(_ => jump.Play())
                .AddTo(this);
            PlayerStats.IsRunning
                .IfTrue()
                .Subscribe(_ => sprinting.Play())
                .AddTo(this);
            PlayerStats.IsRunning
                .IfFalse()
                .Subscribe(_ => sprinting.Stop())
                .AddTo(this);
        }
    }
}