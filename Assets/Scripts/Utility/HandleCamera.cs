using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class HandleCamera : MonoBehaviour
{
    [SerializeField] private float rotationAmount;
    [SerializeField] private float rotationDuration;
    private bool rotating = false;
    private IEnumerator RotateCamera(Vector3 angle)
    {
        rotating = true;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(transform.eulerAngles + angle);

        for (var t = 0f; t <= 1; t += Time.deltaTime / rotationDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }

        transform.rotation = endRotation;
        rotating = false;
    }

    void OnRotateCamera(InputValue value)
    {
        if (!rotating)
        {
            StartCoroutine(RotateCamera(Vector3.up * rotationAmount));
        }
    }
}
