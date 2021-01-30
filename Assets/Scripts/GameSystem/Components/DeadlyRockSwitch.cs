using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem.Dto;
using GameSystem;

public class DeadlyRockSwitch : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private bool destroyOnCollide = false;


    private void Start()
    {
        rb.isKinematic = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.IsPlayerCollision()) return;

        rb.isKinematic = false;
        if (destroyOnCollide)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.IsPlayerCollision()) return;
        rb.isKinematic = false;
        if (destroyOnCollide)
        {
            Destroy(gameObject);
        }
    }
}
