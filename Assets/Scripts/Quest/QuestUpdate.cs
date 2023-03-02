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
                Debug.Log(quest.QuestName + " 수락했습니다");
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
