using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    public static SceneData Inst = null;
    public Transform BatAttackPos;

    private void Awake()
    {
        Inst = this;
    }
}
