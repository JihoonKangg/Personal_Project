using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Quest2 : QuestStart
{
    [SerializeField] Item Successitem; //성공 시 받는 아이템

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
                    if (SceneData.Inst.myquest.slots[i].quest != quest) //퀘스트가 안맞는지 확인
                    {
                        if (SceneData.Inst.myquest.slots[i].quest == null) //퀘스트가 존재하는지 확인
                        {
                            SceneData.Inst.myquest.AcquireQuest(quest);
                            gameObject.GetComponent<Animator>().SetTrigger("Talk");
                            Debug.Log(quest.QuestName + " 수락했습니다");
                            dia = transform.GetComponent<DialogueTrigger>().info;
                            transform.GetComponent<DialogueTrigger>().Trigger(dia);
                            return;
                        }
                    }
                    else //퀘스트가 맞을 때
                    {
                        if(SceneData.Inst.myquest.slots[i].quest.questType == Quest.QuestType.Item) //퀘스트 타입이 아이템인 경우
                        {
                            if (SceneData.Inst.myquest.slots[i].success) // 퀘스트 성공
                            {
                                dia = transform.GetComponent<DialogueTrigger>().Successinfo;
                                transform.GetComponent<DialogueTrigger>().Trigger(dia);
                                Debug.Log("퀘스트 성공");
                                SceneData.Inst.myquest.slots[i].QuestSuccess(); //퀘스트 성공하여 슬롯 초기화
                                quest.Success = true;
                                UseItem();
                                SceneData.Inst.myinven.AcquireItem(Successitem, 5);
                                return;
                            }
                            else //퀘스트 진행중
                            {
                                dia = transform.GetComponent<DialogueTrigger>().Alreadyinfo;
                                transform.GetComponent<DialogueTrigger>().Trigger(dia);
                                Debug.Log("이미 퀘스트를 수락하였습니다");
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
        if (myTarget == other.transform) //아직 있는 상태
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
                quest.QuestCode == SceneData.Inst.myquest.slots[i].quest.QuestCode) //퀘스트 타입 확인 + 퀘스트 코드가 같은 경우
            {
                for(int j = 0; j < SceneData.Inst.myinven.slots.Length; j++)
                {
                    if (SceneData.Inst.myinven.slots[j].item == null) return;
                    if (quest.NeedItemCode == SceneData.Inst.myinven.slots[j].item.itemCode) //아이템 코드가 같은 경우
                        SceneData.Inst.myquest.slots[i].neednum = SceneData.Inst.myinven.slots[j].itemCount;
                    if (SceneData.Inst.myquest.slots[i].neednum >= quest.NeedCount) //퀘스트를 성공했을 때
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
