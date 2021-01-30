using GameSystem.Dto;
using UnityEngine;

namespace GameSystem.Components
{
    public class LoseSanityPerFixedUpdate : MonoBehaviour
    {
        public float amount;

        private void FixedUpdate()
        {
            GameEvents.LowerSanity(SanityAmount);
        }
        
        private SanityDto SanityAmount => new SanityDto
        {
            amount = amount
        };
    }
}