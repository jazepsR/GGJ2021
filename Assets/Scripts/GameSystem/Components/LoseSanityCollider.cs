using GameSystem.Dto;
using UnityEngine;

namespace GameSystem.Components
{
    public class LoseSanityCollider : MonoBehaviour
    {
        [SerializeField] private float amount;
        
        private void OnCollisionEnter(Collision other)
        {
            if (Utility.IsPlayerCollision(other))
            {
                GameEvents.LowerSanity(SanityLost);
            }
        }

        private SanityDto SanityLost => new SanityDto
        {
            amount = amount
        };
    }
}