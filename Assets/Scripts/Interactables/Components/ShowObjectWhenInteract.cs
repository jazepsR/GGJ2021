using UniRx;
using UnityEngine;

namespace Interactables.Components
{
    public class ShowObjectWhenInteract : MonoBehaviour
    {
        [SerializeField] private InteractableCollider interactableCollider;
        [SerializeField] private GameObject activeObject;
        [SerializeField] private bool shouldToggle;

        private void Awake()
        {
            interactableCollider.WasInteractedWith
                .Subscribe(_ => ActiveObject())
                .AddTo(this);

            interactableCollider.PlayerLeft
                .Subscribe(_ => DeactivateObject())
                .AddTo(this);
        }

        private void DeactivateObject()
        {
            activeObject.SetActive(false);
        }

        private void ActiveObject()
        {
            activeObject.SetActive(!shouldToggle || !activeObject.activeSelf);
            interactableCollider.SetInteractButton(!activeObject.activeSelf);
        }
    }
}