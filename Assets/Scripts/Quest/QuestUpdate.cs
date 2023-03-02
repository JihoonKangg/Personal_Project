using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class QuestUpdate : MonoBehaviour
{
    public Quest quest;
    private bool pickupActivated = false; //���� ������ �� true

    [SerializeField]
    private LayerMask layerMask; //������ ���̾ �ߵ�
    private Transform myTarget;

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log(quest.QuestName + " �����߽��ϴ�");
                SceneData.Inst.myquest.AcquireQuest(quest);
                pickupActivated = false;
                gameObject.GetComponent<Animator>().SetTrigger("Talk");
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (myTarget != null) return;
        if ((layerMask & 1 << other.gameObject.layer) != 0)
        {
            myTarget = other.transform;
            pickupActivated = true;
            SceneData.Inst.actionText.gameObject.SetActive(true);
            SceneData.Inst.actionText.text = "NPC�� ��ȭ " + "<color=yellow>" + "(F)" + "</color>";
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (myTarget == other.transform) //���� �ִ� ����
        {
            CanPickUp();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (myTarget == other.transform)//Ÿ���� ��������
        {
            Debug.Log("��ȭ �Ұ�");
            myTarget = null;
            pickupActivated = false;
            SceneData.Inst.actionText.gameObject.SetActive(false);
        }
    }
}
