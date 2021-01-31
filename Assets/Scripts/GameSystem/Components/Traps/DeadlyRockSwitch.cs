using System.Collections;
using UnityEngine;

namespace GameSystem.Components.Traps
{
    public class DeadlyRockSwitch : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private bool destroyOnCollide = false;
        [SerializeField] private float delay = 0f;

        private void Start()
        {
            rb.isKinematic = true;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!other.IsPlayerCollision()) return;
            StartCoroutine(EnableBoulder(delay));

        }

        private IEnumerator EnableBoulder(float delay)
        {
            yield return new WaitForSeconds(delay);
            rb.isKinematic = false;
            if (destroyOnCollide)
            {
                Destroy(gameObject);
            }

        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.IsPlayerCollision()) return;
            StartCoroutine(EnableBoulder(delay));
        }
    }
}
