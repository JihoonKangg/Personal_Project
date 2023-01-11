using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHP : CharacterProperty
{
    public Transform myTarget;
    public Slider myBar;
    public Slider myBGBar;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(myTarget.position); //3차원 공간의 월드좌표를 모니터상의 좌표로 바꿔줌
        transform.position = pos;
    }
}
