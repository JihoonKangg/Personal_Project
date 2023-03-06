using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class NPCSystem : MonoBehaviour
{
    [SerializeField]
    private LayerMask myPlayer;
    private Transform myTarget;
    private bool CanActivated = false; //�ռ��� ��� ������ �� true
    [SerializeField]
    private GameObject mySynthesis;
    [SerializeField]
    private MainSlot myMainSlot;
    [SerializeField]
    private TMP_Text actionText; //�ʿ��� ������Ʈ
    private Dialogue info = null;

    private void CanUse()
    {
        if (CanActivated)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                info = transform.GetComponent<DialogueTrigger>().info;
                transform.GetComponent<DialogueTrigger>().Trigger(info, ()=>UiActivate());
            }
        }
    }

    public void UiActivate()
    {
        Debug.Log("�Լ�����");
        SceneData.Inst.Synthesis.SetActive(true);
        CanActivated = false;
        myTarget = null;
        actionText.gameObject.SetActive(CanActivated);
        myMainSlot.Check();
        SceneData.Inst.OnUI = !CanActivated;
        SceneData.Inst.UIOn();
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
        if (myTarget == other.transform)//Ÿ���� ��������
        {
            Debug.Log("���ֿ�");
            CanActivated = false;
            myTarget = null;
            actionText.gameObject.SetActive(false);
        }
    }
}
