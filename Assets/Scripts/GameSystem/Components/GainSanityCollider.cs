using GameSystem.Dto;
using UnityEngine;

namespace GameSystem.Components
{
    public class GainSanityCollider : MonoBehaviour
    {
        [SerializeField] private float amount;
        
        private void OnCollisionEnter(Collision other)
        {
            if (Utility.IsPlayerCollision(other))
            {
                GameEvents.GainSanity(SanityGain);
            }
        }

        private SanityDto SanityGain => new SanityDto
        {
            amount = amount
        };
    }
}