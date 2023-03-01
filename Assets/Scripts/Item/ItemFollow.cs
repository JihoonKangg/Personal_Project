using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFollow : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.position = transform.parent.position;
    }
}
