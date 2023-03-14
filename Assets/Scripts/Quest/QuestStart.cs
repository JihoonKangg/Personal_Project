using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStart : MonoBehaviour
{
    public Quest quest;
    protected bool pickupActivated = false; //���� ������ �� true

    [SerializeField]
    protected LayerMask layerMask; //������ ���̾ �ߵ�
    protected Transform myTarget;
    protected Dialogue dia = null;


    protected void OnTriggerEnter(Collider other)
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

    protected void OnTriggerExit(Collider other)
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
