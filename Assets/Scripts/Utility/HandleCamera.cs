using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class HandleCamera : MonoBehaviour
{
    [SerializeField] private float rotationAmount;
    [SerializeField] private float rotationDuration;
    private IEnumerator RotateCamera()
    {
        float time = 0;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(startRotation.x, startRotation.y + rotationAmount, startRotation.z);

        while (time < rotationDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time / rotationDuration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
    }

    void OnRotateCam(InputValue value)
    {
        RotateCamera();
    }
}
