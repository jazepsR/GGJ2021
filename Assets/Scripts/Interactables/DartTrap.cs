using GameSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartTrap : MonoBehaviour
{
    [SerializeField] private GameObject dart;
    [SerializeField] private Transform[] dartPoints;
    [SerializeField] private float dartFireDelay;
    [SerializeField] private float delayBetweenDarts;
    [SerializeField] private Animator switchAnim;
    private bool firedDarts = false;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (!other.IsPlayerCollision() || firedDarts) return;
        StartCoroutine(FireDarts(dartFireDelay));

    }

    private IEnumerator FireDarts(float delay)
    {
        firedDarts = true;
        switchAnim.SetTrigger("press");
        yield return new WaitForSeconds(delay);
        foreach(Transform dartPoint in dartPoints)
        {
            Instantiate(dart, dartPoint.position, dartPoint.rotation);
            yield return new WaitForSeconds(dartFireDelay);

        }

    }
}
