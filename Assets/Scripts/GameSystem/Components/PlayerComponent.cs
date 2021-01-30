using UnityEngine;

namespace GameSystem.Components
{
    public class PlayerComponent : MonoBehaviour
    {
        private void Awake()
        {
            GameStateManager.CurrentPlayerSet = this;
        }
    }
}