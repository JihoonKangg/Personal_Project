using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCSystem : MonoBehaviour
{
    [SerializeField]
    private LayerMask myPlayer;
    private Transform myTarget;
    private bool CanActivated = false; //�ռ��� ��� ������ �� true
    [SerializeField]
    private Synthesis mySynthesis;

    [SerializeField]
    private TMP_Text actionText; //�ʿ��� ������Ʈ


    private void CanUse()
    {
        if (CanActivated)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("NPC�� �����մϴ�");
                actionText.gameObject.SetActive(false);
                CanActivated = false;
                //mySynthesis.SetActive(true);
                mySynthesis.CanUpdate();
                myTarget = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (myTarget != null) return;
        if ((myPlayer & 1 << other.gameObject.layer) != 0)
        {
            myTarget = other.transform;
            CanActivated = true;
            Debug.Log("���� �� ����");
            actionText.gameObject.SetActive(true);
            actionText.text = "�ռ���" + "<color=yellow>" + "(F)" + "</color>";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (myTarget == other.transform)
        {
            CanUse();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (myTarget != null) return;
        if (myTarget == other.transform)//Ÿ���� ��������
        {
            Debug.Log("���ֿ�");
            CanActivated = false;
            myTarget = null;
            actionText.gameObject.SetActive(false);
        }
    }
}
