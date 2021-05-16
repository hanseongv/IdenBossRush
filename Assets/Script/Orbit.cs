using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;

    public float orbitSpeed;
    private Vector3 offSet;

    private void Start()
    {
        offSet = transform.position - target.position;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = target.position + offSet;
        transform.RotateAround(target.position, Vector3.up, orbitSpeed * Time.deltaTime);
        offSet = transform.position - target.position;
    }
}