using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = target.position + offset;
    }
}