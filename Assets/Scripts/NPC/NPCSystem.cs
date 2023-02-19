using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSystem : MonoBehaviour
{
    [SerializeField]
    private LayerMask myPlayer;
    private Transform myTarget;
    private bool CanActivated = false; //�ռ��� ��� ������ �� true


    private void CanPickUp()
    {
        if (CanActivated)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log(myTarget.GetComponent<ItemPickUp>().item.itemName + " ȹ���߽��ϴ�");
                CanActivated = false;
                Destroy(myTarget.gameObject);
                myTarget = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (myTarget != null) return;
        if ((myPlayer & 1 << other.gameObject.layer) != 0)
        {
            myTarget = other.transform;
            CanActivated = true;
            Debug.Log("���� �� ����");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (myTarget == other.transform)
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (myTarget != null) return;
        if (myTarget == other.transform)//Ÿ���� ��������
        {
            Debug.Log("���ֿ�");
            CanActivated = false;
            myTarget = null;
        }
    }
}
