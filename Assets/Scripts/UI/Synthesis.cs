using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synthesis : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneData.Inst.Synthesis.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    public void Exit()
    {
        SceneData.Inst.Synthesis.SetActive(false);
        SceneData.Inst.OnUI = false;
        SceneData.Inst.UIOn();
    }
}
