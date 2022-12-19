using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public Transform PlayerTarget;
    // Update is called once per frame
    void Update()
    {
        this.transform.position = PlayerTarget.position;
    }
}
