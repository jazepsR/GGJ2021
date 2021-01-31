using UniRx;
using UnityEngine;

namespace GameSystem.Components.Core
{
    public class PlayerCollider : MonoBehaviour
    {
        [SerializeField] private AudioSource sfx;
        
        private readonly IReactiveProperty<bool> _playerInCollider = new ReactiveProperty<bool>();

        public IReadOnlyReactiveProperty<bool> PlayerInCollider => _playerInCollider;

        private void OnTriggerEnter(Collider other)
        {
            PlayerDidEnterCollider(other, true);
        }

        private void OnTriggerExit(Collider other)
        {
            PlayerDidEnterCollider(other, false);
        }

        private void OnCollisionEnter(Collision other)
        {
            PlayerDidEnterCollider(other, true);
        }

        private void OnCollisionExit(Collision other)
        {
            PlayerDidEnterCollider(other, false);
        }

        private void PlayerDidEnterCollider(Collider other, bool value)
        {
            if (!other.IsPlayerCollision())
            {
                return;
            }

            _playerInCollider.Value = value;
            if (value)
            {
                sfx?.Play();
            }
            else
            {
                sfx?.Stop();
            }
        }
        
        private void PlayerDidEnterCollider(Collision other, bool value)
        {
            if (!other.IsPlayerCollision())
            {
                return;
            }

            _playerInCollider.Value = value;
            if (value)
            {
                sfx?.Play();
            }
            else
            {
                sfx?.Stop();
            }
        }
    }
}