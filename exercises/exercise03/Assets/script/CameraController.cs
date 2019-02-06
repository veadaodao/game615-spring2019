using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject whiteball;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - whiteball.transform.position;
    }

    void LateUpdate()
    {
        transform.position = whiteball.transform.position + offset;
    }
}
