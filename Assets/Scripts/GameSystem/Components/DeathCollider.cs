using GameSystem.Dto;
using UnityEngine;

namespace GameSystem.Components
{
    public class DeathCollider : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (Utility.IsPlayerCollision(other))
            {
                GameEvents.KillPlayer(GetPlayerDeathDto());
            }
        }

        private PlayerDeathDto GetPlayerDeathDto()
        {
            return new PlayerDeathDto();
        }
    }
}