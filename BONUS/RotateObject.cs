using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float RotationSpeed = 20.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right * RotationSpeed * Time.deltaTime +
                       Vector3.up * RotationSpeed * Time.deltaTime +
                       Vector3.forward * RotationSpeed * Time.deltaTime, Space.Self);

    }
}
