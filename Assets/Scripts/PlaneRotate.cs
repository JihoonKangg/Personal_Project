using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneRotate : MonoBehaviour
{
    public float MoveSpeed = 15.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, MoveSpeed * Time.deltaTime, Space.World);
    }
}
