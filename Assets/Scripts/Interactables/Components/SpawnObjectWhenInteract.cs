using UniRx;
using UnityEngine;

namespace Interactables.Components
{
    public class SpawnObjectWhenInteract : MonoBehaviour
    {
        [SerializeField] private InteractableCollider interactableCollider;
        [SerializeField] private GameObject objectToSpawn;
        private void Awake()
        {
            interactableCollider.WasInteractedWith
                .Subscribe(_ => ActiveObject())
                .AddTo(this);
        }
        
        private void ActiveObject()
        {
            Instantiate(objectToSpawn, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}