using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offSet = new Vector3(0, 21, -15);
    void Start()
    {
        
    }

    
    void LateUpdate()
    {
        transform.position = PlayerController.instance.transform.position + offSet;
    }
}
