using UnityEngine;

namespace GameSystem
{
    internal static class Utility
    {
        private const string PlayerTag = "player";
        
        internal static bool IsPlayerCollision(Collision other)
        {
            return other.gameObject.CompareTag(PlayerTag);
        }
    }
}