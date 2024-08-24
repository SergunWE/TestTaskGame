using System;
using System.Collections;
using UnityEngine;

public class ObjectRotater : MonoBehaviour
{
    [SerializeField] private float rotationDuration = 1f;
    [SerializeField] private float maxSpeedMultiplier = 3;
    [SerializeField] private float additionSpeed = 1;
    [SerializeField] private float stopDuration = 0.5f;
    [SerializeField] private float speedUpOffset = 10f;

    private float _currentMultiplier;
    private bool _clockwise;
    private float _targetRotation;

    private void Awake()
    {
        _targetRotation = transform.eulerAngles.z;
    }

    public void Reset()
    {
        
    }

    public void Rotate(float rotation, bool clockwise)
    {
        StopAllCoroutines();
        _targetRotation = rotation;
        StartCoroutine(RotateCoroutine(clockwise));
    }

    private IEnumerator RotateCoroutine(bool clockwise)
    {
        if (_currentMultiplier != 0 && _clockwise != clockwise)
        {
            _currentMultiplier = 0;
            yield return new WaitForSeconds(stopDuration);
        }

        _clockwise = clockwise;

        if (Mathf.Abs(_targetRotation - transform.eulerAngles.z) > 90f - speedUpOffset)
        {
            _currentMultiplier = clockwise ? _currentMultiplier + additionSpeed : _currentMultiplier - additionSpeed;
            _currentMultiplier = Mathf.Clamp(_currentMultiplier, -maxSpeedMultiplier, maxSpeedMultiplier);
        }
        else
        {
            _currentMultiplier = clockwise ? 1f : -1f;
        }

        float currentAngle = transform.eulerAngles.z;
        float arcAngle;

        if (clockwise)
        {
            arcAngle = _targetRotation < currentAngle ? _targetRotation + 360 : _targetRotation;
        }
        else
        {
            arcAngle = _targetRotation > currentAngle ? _targetRotation - 360 : _targetRotation;
        }

        float startAngle = currentAngle;
        float endAngle = arcAngle;
        float elapsedTime = 0f;

        while (elapsedTime < rotationDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAngle = Mathf.LerpAngle(startAngle, endAngle, elapsedTime / rotationDuration);
            transform.eulerAngles = new Vector3(0, 0, newAngle);
            yield return null;
        }

        transform.eulerAngles = new Vector3(0, 0, _targetRotation);
    }
}

