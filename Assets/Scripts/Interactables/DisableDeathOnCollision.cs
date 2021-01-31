using System.Collections;
using System.Collections.Generic;
using GameSystem.Components.Traps;
using UnityEngine;

public class DisableDeathOnCollision : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            GetComponent<DeathCollider>().active = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
