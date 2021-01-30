using GameSystem.Dto;
using UnityEngine;

namespace GameSystem.Components
{
    public abstract class SanityCollider : MonoBehaviour
    {
        [SerializeField] private float amount;
        [SerializeField] private bool destroyOnCollide;
        
        private void OnCollisionEnter(Collision other)
        {
            if (!other.IsPlayerCollision()) return;
            
            OnPlayerCollide();
            
            if (destroyOnCollide)
            {
                Destroy(this);
            }
        }

        protected abstract void OnPlayerCollide();

        protected SanityDto SanityAmount => new SanityDto
        {
            amount = amount
        };
    }
}