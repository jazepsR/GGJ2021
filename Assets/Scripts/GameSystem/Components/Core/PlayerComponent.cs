using UniRx;
using UnityEngine;

namespace GameSystem.Components.Core
{
    public class PlayerComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource jump;
        
        private void Awake()
        {
            GameStateManager.currentPlayerSet = this;
            PlayerStats.IsJumping
                .IfTrue()
                .Subscribe(_ => jump.Play())
                .AddTo(this);
        }
    }
}