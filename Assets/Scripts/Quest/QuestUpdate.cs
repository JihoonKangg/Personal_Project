using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class QuestUpdate : MonoBehaviour
{
    public Quest quest;
    private bool pickupActivated = false; //습득 가능할 시 true

    [SerializeField]
    private LayerMask layerMask; //아이템 레이어만 발동
    private Transform myTarget;

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (SceneData.Inst.myquest.slots[0].quest == null)
                {
                    Debug.Log(quest.QuestName + " 수락했습니다");
                    SceneData.Inst.myquest.AcquireQuest(quest);
                    pickupActivated = false;
                    gameObject.GetComponent<Animator>().SetTrigger("Talk");
                    return;
                }

                for (int i = 0; i < SceneData.Inst.myquest.slots.Length; i++)
                {
                    if (SceneData.Inst.myquest.slots[i] == null) return;

                    if (SceneData.Inst.myquest.slots[i].success) // 성공했다는 bool값 호출
                    {
                        Debug.Log("퀘스트 성공");
                        SceneData.Inst.myquest.slots[i].QuestSuccess(); //퀘스트 성공하여 슬롯 초기화
                        return;
                    }
                    else Debug.Log("이미 퀘스트를 수락하였습니다");
                    //if (SceneData.Inst.myquest.slots[i].quest.QuestCode == quest.QuestCode) return; //퀘스트를 이미 수락한 경우
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
            SceneData.Inst.actionText.text = "NPC와 대화 " + "<color=yellow>" + "(F)" + "</color>";
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (myTarget == other.transform) //아직 있는 상태
        {
            CanPickUp();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (myTarget == other.transform)//타겟이 빠져나감
        {
            Debug.Log("대화 불가");
            myTarget = null;
            pickupActivated = false;
            SceneData.Inst.actionText.gameObject.SetActive(false);
        }
    }
}
