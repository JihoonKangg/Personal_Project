using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quest1 : QuestStart
{
    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if(quest.Success)
                {
                    dia = transform.GetComponent<DialogueTrigger>().AlreadySuccessinfo;
                    transform.GetComponent<DialogueTrigger>().Trigger(dia);
                    return;
                }

                pickupActivated = false;
                for (int i = 0; i < SceneData.Inst.myquest.slots.Length; i++)
                {
                    if(SceneData.Inst.myquest.slots[i].quest != quest) //����Ʈ�� �ȸ´��� Ȯ��
                    {
                        if(SceneData.Inst.myquest.slots[i].quest == null) //����Ʈ�� �����ϴ��� Ȯ��
                        {
                            SceneData.Inst.myquest.AcquireQuest(quest);
                            gameObject.GetComponent<Animator>().SetTrigger("Talk");
                            Debug.Log(quest.QuestName + " �����߽��ϴ�");
                            dia = transform.GetComponent<DialogueTrigger>().info;
                            transform.GetComponent<DialogueTrigger>().Trigger(dia);
                            return;
                        }
                    }
                    else //����Ʈ�� ���� ��
                    {
                        if(SceneData.Inst.myquest.slots[i].success) // ����Ʈ ����
                        {
                            dia = transform.GetComponent<DialogueTrigger>().Successinfo;
                            transform.GetComponent<DialogueTrigger>().Trigger(dia);
                            Debug.Log("����Ʈ ����");
                            SceneData.Inst.myquest.slots[i].QuestSuccess(); //����Ʈ �����Ͽ� ���� �ʱ�ȭ
                            quest.Success = true;
                            return;
                        }
                        else //����Ʈ ������
                        {
                            dia = transform.GetComponent<DialogueTrigger>().Alreadyinfo;
                            transform.GetComponent<DialogueTrigger>().Trigger(dia);
                            Debug.Log("�̹� ����Ʈ�� �����Ͽ����ϴ�");
                            return;
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (myTarget == other.transform) //���� �ִ� ����
        {
            CanPickUp();
        }
    }
}
