using System;
using GameSystem;
using UniRx;
using UnityEngine;

namespace Interactables.Components
{
    public class InteractableCollider : MonoBehaviour
    {
        private readonly SerialDisposable _disposable = new SerialDisposable();
        private readonly ISubject<Unit> _wasInteractedWith = new Subject<Unit>();
        private readonly ISubject<Unit> _playerLeft = new Subject<Unit>();
        
        [SerializeField] private GameObject showInteractButton;

        public IObservable<Unit> WasInteractedWith => _wasInteractedWith;

        public IObservable<Unit> PlayerLeft => _playerLeft;

        public void SetInteractButton(bool value) => showInteractButton.SetActive(value);

        private void OnTriggerEnter(Collider other)
        {
            if (!other.IsPlayerCollision())
            {
                return;
            }
            SetInteractButton(true);
            _disposable.Disposable = KeyPress.InteractionPressed.Subscribe(_wasInteractedWith.OnNext);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.IsPlayerCollision())
            {
                return;
            }
            SetInteractButton(false);
            _playerLeft.OnNext(Unit.Default);
            _disposable.Disposable = null;
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (!other.IsPlayerCollision())
            {
                return;
            }
            SetInteractButton(true);
            _disposable.Disposable = KeyPress.InteractionPressed.Subscribe(_wasInteractedWith.OnNext);
        }

        private void OnCollisionExit(Collision other)
        {
            if (!other.IsPlayerCollision())
            {
                return;
            }
            SetInteractButton(false);
            _playerLeft.OnNext(Unit.Default);
            _disposable.Disposable = null;
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}