using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quest1 : QuestStart
{
    private void CanPickUp(Collider other)
    {
        if (pickupActivated)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(RotateTarget(other));
                if(Success)
                {
                    dia = transform.GetComponent<DialogueTrigger>().AlreadySuccessinfo;
                    transform.GetComponent<DialogueTrigger>().Trigger(dia);
                    return;
                }

                pickupActivated = false;
                for (int i = 0; i < SceneData.Inst.myquest.slots.Length; i++)
                {
                    if(SceneData.Inst.myquest.slots[i].quest != quest) //퀘스트가 안맞는지 확인
                    {
                        if(SceneData.Inst.myquest.slots[i].quest == null) //퀘스트가 존재하는지 확인
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
                        if(SceneData.Inst.myquest.slots[i].success) // 퀘스트 성공
                        {
                            dia = transform.GetComponent<DialogueTrigger>().Successinfo;
                            transform.GetComponent<DialogueTrigger>().Trigger(dia);
                            Debug.Log("퀘스트 성공");
                            SceneData.Inst.myquest.slots[i].QuestSuccess(); //퀘스트 성공하여 슬롯 초기화
                            SceneData.Inst.PlayerLevel.EXP += 50;
                            SceneData.Inst.ExpSlider.GetComponent<Animator>().SetTrigger("Show");
                            Success = true;
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

    private void OnTriggerStay(Collider other)
    {
        if (myTarget == other.transform) //아직 있는 상태
        {
            CanPickUp(other);
        }
    }

    
}
