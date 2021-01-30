using GameSystem.Dto;
using UnityEngine;

namespace GameSystem.Components
{
    public class DeathCollider : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.IsPlayerCollision())
            {
                GameEvents.KillPlayer(GetPlayerDeathDto());
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.IsPlayerCollision())
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