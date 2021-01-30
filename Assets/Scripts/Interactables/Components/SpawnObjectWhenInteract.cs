using UniRx;
using UnityEngine;

namespace Interactables.Components
{
    public class SpawnObjectWhenInteract : MonoBehaviour
    {
        [SerializeField] private InteractableCollider interactableCollider;
        [SerializeField] private GameObject objectToSpawn;
        [SerializeField] private float zForwardPosition;
        [SerializeField] private float yLeftPosition;
        
        private void Awake()
        {
            interactableCollider.WasInteractedWith
                .Subscribe(_ => ActiveObject())
                .AddTo(this);
        }
        
        private void ActiveObject()
        {
            Instantiate(objectToSpawn, transform.position + 
                                       (Vector3.forward * zForwardPosition) + (Vector3.left * yLeftPosition), 
                Quaternion.identity);
            Destroy(gameObject);
        }
    }
}