using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem
{
    public static class Utility
    {
        private const string PlayerTag = "Player";
        
        public static bool IsPlayerCollision(this Collision other) => other.gameObject.CompareTag(PlayerTag);
        public static bool IsPlayerCollision(this Collider other) => other.gameObject.CompareTag(PlayerTag);
        public static bool IsPlayScene(this Scene scene, string gameSceneName) => scene.name == gameSceneName;
    }
}