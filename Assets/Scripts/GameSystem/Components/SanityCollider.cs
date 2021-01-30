using GameSystem.Dto;
using UnityEngine;

namespace GameSystem.Components
{
    internal abstract class SanityCollider : MonoBehaviour
    {
        [SerializeField] private float amount;
        [SerializeField] private bool destroyOnCollide;
        
        private void OnCollisionEnter(Collision other)
        {
            if (!Utility.IsPlayerCollision(other)) return;
            
            OnPlayerCollide();
            
            if (destroyOnCollide)
            {
                Destroy(this);
            }
        }

        protected abstract void OnPlayerCollide();

        private SanityDto SanityGain => new SanityDto
        {
            amount = amount
        };
    }
}