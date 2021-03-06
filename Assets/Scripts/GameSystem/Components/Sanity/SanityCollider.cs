using GameSystem.Dto;
using UnityEngine;

namespace GameSystem.Components.Sanity
{
    public abstract class SanityCollider : MonoBehaviour
    {
        [SerializeField] private float amount;
        [SerializeField] private bool destroyOnCollide;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.IsPlayerCollision()) return;
            
            OnPlayerCollide();
            
            if (destroyOnCollide)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.IsPlayerCollision()) return;
            
            OnPlayerCollide();
            
            if (destroyOnCollide)
            {
                Destroy(gameObject);
            }
        }

        protected abstract void OnPlayerCollide();

        protected SanityDto SanityAmount => new SanityDto
        {
            amount = amount
        };
    }
}