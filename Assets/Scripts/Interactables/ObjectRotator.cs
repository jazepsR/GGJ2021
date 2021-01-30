using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Vector3 rotationSpeed;
    [SerializeField] private AnimationCurve xCurve;
    [SerializeField] private AnimationCurve yCurve;
    [SerializeField] private AnimationCurve zCurve;
    [SerializeField] private float curveMoveTime= 1f;
    private Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
        StartCoroutine(TweenPosition());
    }

   
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    private IEnumerator TweenPosition()
    {
        float t = 0;
        while(t<1)
        {
            yield return null;
            t += Time.deltaTime / curveMoveTime;
            transform.position = startPos + new Vector3(xCurve.Evaluate(t), yCurve.Evaluate(t), zCurve.Evaluate(t));
        }
        StartCoroutine(TweenPosition());
    }
}
