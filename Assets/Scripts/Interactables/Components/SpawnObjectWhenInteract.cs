using System;
using UniRx;
using UnityEngine;

namespace Interactables.Components
{
    public class SpawnObjectWhenInteract : MonoBehaviour
    {
        [SerializeField] private InteractableCollider interactableCollider;
        [SerializeField] private GameObject objectToSpawn;
        [SerializeField] private float zForwardPosition;
        [SerializeField] private float xLeftPosition;
        [SerializeField] private float yDownPosition;
        [SerializeField] private AudioSource interactSound;
        
        private void Awake()
        {
            interactableCollider.WasInteractedWith
                .Subscribe(_ => ActiveObject())
                .AddTo(this);
        }
        
        private void ActiveObject()
        {
            if (interactSound != null)
            {
                var length = interactSound.clip.length;
                interactSound.Play();
                Observable.Return(Unit.Default).Delay(TimeSpan.FromSeconds(length))
                    .Take(1)
                    .Subscribe(_ =>
                    {
                        Instantiate(objectToSpawn, transform.position + 
                                                   (Vector3.forward * zForwardPosition) + 
                                                   (Vector3.left * xLeftPosition) + 
                                                   (Vector3.down * yDownPosition), 
                            Quaternion.identity);
                        Destroy(gameObject);
                    }).AddTo(this);
            }
            else
            {
                Instantiate(objectToSpawn, transform.position + 
                                           (Vector3.forward * zForwardPosition) + 
                                           (Vector3.left * xLeftPosition) + 
                                           (Vector3.down * yDownPosition), 
                    Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}