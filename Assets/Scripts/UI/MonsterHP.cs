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
        Vector3 pos = Camera.main.WorldToScreenPoint(myTarget.position); //3���� ������ ������ǥ�� ����ͻ��� ��ǥ�� �ٲ���
        transform.position = pos;
    }
}
