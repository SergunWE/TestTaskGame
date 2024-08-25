using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotater : MonoBehaviour
{
    [SerializeField] private Transform movableObject;
    [SerializeField] private Vector3 angle;
    [SerializeField] private float rotateTime = 1.0f;
    [SerializeField] private float delayBetweenMoves = 1.0f;
    [SerializeField] private float startDelay;
    [SerializeField] private AnimationCurve movementCurve;
    [SerializeField] private bool loop;
    private void Start()
    {
        StartCoroutine(MoveObject());
    }

    private IEnumerator MoveObject()
    {
        yield return new WaitForSeconds(startDelay);
        do
        {
            yield return StartCoroutine(RotateCoroutine(movableObject.eulerAngles, movableObject.eulerAngles + angle));
            yield return new WaitForSeconds(delayBetweenMoves);
        } while (loop);
    }

    private IEnumerator RotateCoroutine(Vector3 fromRotation, Vector3 toRotation)
    {
        float elapsedTime = 0;
        while (elapsedTime < rotateTime)
        {
            elapsedTime += Time.deltaTime;
            float t = movementCurve.Evaluate(elapsedTime / rotateTime);
            movableObject.eulerAngles = Vector3.Lerp(fromRotation, toRotation, t);
            yield return null;
        }
        movableObject.eulerAngles = toRotation;
    }
}
