using UnityEngine;

namespace GameSystem.Components.Core
{
    public class PlayerComponent : MonoBehaviour
    {
        private void Awake()
        {
            GameStateManager.currentPlayerSet = this;
        }
    }
}