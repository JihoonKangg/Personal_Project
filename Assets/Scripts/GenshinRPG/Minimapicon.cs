using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimapicon : MonoBehaviour
{
    [SerializeField] Transform player;
    void Update()
    {
        transform.rotation = player.GetComponentInChildren<Transform>().rotation;
    }
}
