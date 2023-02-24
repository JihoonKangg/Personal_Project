using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synthesis : MonoBehaviour
{
    public struct HowCanUseThis
    {
        public bool UseThis;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    public void CanUpdate()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void Exit()
    {
        gameObject.SetActive(false);
        if (Time.timeScale == 1.0f) return;
        Time.timeScale = 1.0f;
    }
}
