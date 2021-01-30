using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem
{
    internal static class Utility
    {
        private const string PlayerTag = "player";
        
        internal static bool IsPlayerCollision(this Collision other) => other.gameObject.CompareTag(PlayerTag);
        internal static bool IsPlayScene(this Scene scene, string gameSceneName) => scene.name == gameSceneName;
    }
}