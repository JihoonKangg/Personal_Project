using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Quest2 : QuestStart
{
    [SerializeField] Item Successitem; //���� �� �޴� ������

    void Update()
    {
        if (!SceneData.Inst.QuestUI.activeSelf) return;
        else Check();
    }

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Check();
                if (quest.Success)
                {
                    dia = transform.GetComponent<DialogueTrigger>().AlreadySuccessinfo;
                    transform.GetComponent<DialogueTrigger>().Trigger(dia);
                    return;
                }

                pickupActivated = false;
                for (int i = 0; i < SceneData.Inst.myquest.slots.Length; i++)
                {
                    if (SceneData.Inst.myquest.slots[i].quest != quest) //����Ʈ�� �ȸ´��� Ȯ��
                    {
                        if (SceneData.Inst.myquest.slots[i].quest == null) //����Ʈ�� �����ϴ��� Ȯ��
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
                        if(SceneData.Inst.myquest.slots[i].quest.questType == Quest.QuestType.Item) //����Ʈ Ÿ���� �������� ���
                        {
                            if (SceneData.Inst.myquest.slots[i].success) // ����Ʈ ����
                            {
                                dia = transform.GetComponent<DialogueTrigger>().Successinfo;
                                transform.GetComponent<DialogueTrigger>().Trigger(dia);
                                Debug.Log("����Ʈ ����");
                                SceneData.Inst.myquest.slots[i].QuestSuccess(); //����Ʈ �����Ͽ� ���� �ʱ�ȭ
                                quest.Success = true;
                                UseItem();
                                SceneData.Inst.myinven.AcquireItem(Successitem, 5);
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
    }

    private void OnTriggerStay(Collider other)
    {
        if (myTarget == other.transform) //���� �ִ� ����
        {
            CanPickUp();
        }
    }

    public void Check()
    {
        for (int i = 0; i < SceneData.Inst.myquest.slots.Length; i++)
        {
            if (SceneData.Inst.myquest.slots[i].quest == null) return;
            if(SceneData.Inst.myquest.slots[i].quest.questType == Quest.QuestType.Item &&
                quest.QuestCode == SceneData.Inst.myquest.slots[i].quest.QuestCode) //����Ʈ Ÿ�� Ȯ�� + ����Ʈ �ڵ尡 ���� ���
            {
                for(int j = 0; j < SceneData.Inst.myinven.slots.Length; j++)
                {
                    if (SceneData.Inst.myinven.slots[j].item == null) return;
                    if (quest.NeedItemCode == SceneData.Inst.myinven.slots[j].item.itemCode) //������ �ڵ尡 ���� ���
                        SceneData.Inst.myquest.slots[i].neednum = SceneData.Inst.myinven.slots[j].itemCount;
                    if (SceneData.Inst.myquest.slots[i].neednum >= quest.NeedCount) //����Ʈ�� �������� ��
                    {
                        SceneData.Inst.myquest.slots[i].neednum = quest.NeedCount;
                        SceneData.Inst.myquest.slots[i].success = true;
                    }
                }
            }
        }
    }

    private void UseItem()
    {
        for (int j = 0; j < SceneData.Inst.myinven.slots.Length; j++)
        {
            if (SceneData.Inst.myinven.slots[j].item == null) return;
            if (quest.NeedItemCode == SceneData.Inst.myinven.slots[j].item.itemCode)
                SceneData.Inst.myinven.slots[j].Useitem(SceneData.Inst.myinven.slots[j].item, quest.NeedCount);
        }
    }
}
