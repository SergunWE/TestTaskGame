using System.Collections;
using UnityEngine;

public class SmoothMover : MonoBehaviour
{
    [SerializeField] private Transform movableObject;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float travelTime = 1.0f;
    [SerializeField] private float delayBetweenMoves = 1.0f;
    [SerializeField] private AnimationCurve movementCurve;
    [SerializeField] private bool loop;
    [SerializeField] private bool oneWayMove;

    private void Start()
    {
        if(oneWayMove)
        {
            StartCoroutine(MoveObjectFromEnd());
        }
        else
        {
            StartCoroutine(MoveObject());
        }
    }

    private IEnumerator MoveObject()
    {
        do
        {
            yield return StartCoroutine(MoveToPosition(startPoint.position, endPoint.position));
            yield return new WaitForSeconds(delayBetweenMoves);
            yield return StartCoroutine(MoveToPosition(endPoint.position, startPoint.position));
            yield return new WaitForSeconds(delayBetweenMoves);
        } while (loop);
    }

    private IEnumerator MoveObjectFromEnd()
    {
        yield return StartCoroutine(MoveToPosition(startPoint.position, endPoint.position));
        yield return new WaitForSeconds(delayBetweenMoves);
    }

    private IEnumerator MoveToPosition(Vector3 fromPosition, Vector3 toPosition)
    {
        float elapsedTime = 0;
        while (elapsedTime < travelTime)
        {
            elapsedTime += Time.deltaTime;
            float t = movementCurve.Evaluate(elapsedTime / travelTime);
            movableObject.position = Vector3.Lerp(fromPosition, toPosition, t);
            yield return null;
        }
        movableObject.position = toPosition;
    }
}

