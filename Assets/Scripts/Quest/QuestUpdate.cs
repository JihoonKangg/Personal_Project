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
                if (SceneData.Inst.myquest.slots[0].quest == null)
                {
                    Debug.Log(quest.QuestName + " �����߽��ϴ�");
                    SceneData.Inst.myquest.AcquireQuest(quest);
                    pickupActivated = false;
                    gameObject.GetComponent<Animator>().SetTrigger("Talk");
                    return;
                }

                for (int i = 0; i < SceneData.Inst.myquest.slots.Length; i++)
                {
                    if (SceneData.Inst.myquest.slots[i] == null) return;

                    if (SceneData.Inst.myquest.slots[i].success) // �����ߴٴ� bool�� ȣ��
                    {
                        Debug.Log("����Ʈ ����");
                        SceneData.Inst.myquest.slots[i].QuestSuccess(); //����Ʈ �����Ͽ� ���� �ʱ�ȭ
                        return;
                    }
                    else Debug.Log("�̹� ����Ʈ�� �����Ͽ����ϴ�");
                    //if (SceneData.Inst.myquest.slots[i].quest.QuestCode == quest.QuestCode) return; //����Ʈ�� �̹� ������ ���
                }
            }
        }
    }

    private void QuestSuccess()
    {
        //if(quest)
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
