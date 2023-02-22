using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIchecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SceneData.Inst.OnUI = true;
    }
    private void OnTriggerExit(Collider other)
    {
        SceneData.Inst.OnUI = false;
    }
}
