using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range; //���� ������ �ִ� �Ÿ�
    private bool pickupActivated = false; //���� ������ �� true

    Collider col = null;

    [SerializeField]
    private LayerMask layerMask; //������ ���̾ �ߵ�
    private Transform myTarget;
    [SerializeField]
    private Inventory theInventory;

    [SerializeField]
    private TMP_Text actionText; //�ʿ��� ������Ʈ

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log(myTarget.GetComponent<ItemPickUp>().item.itemName + " ȹ���߽��ϴ�");
                theInventory.AcquireItem(myTarget.GetComponent<ItemPickUp>().item, 1);
                pickupActivated = false;
                actionText.gameObject.SetActive(false);
                Destroy(myTarget.gameObject);
                myTarget = null;
            }
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (myTarget != null) return;
        if((layerMask & 1 << other.gameObject.layer) != 0)
        {
            myTarget = other.transform;
            pickupActivated = true;
            actionText.gameObject.SetActive(true);
            actionText.text = other.transform.GetComponent<ItemPickUp>().item.itemName + " ȹ��" + "<color=yellow>" + "(F)" + "</color>";
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(myTarget == other.transform) //���� �ִ� ����
        {
            CanPickUp();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (myTarget != null) return;
        if(myTarget == other.transform)//Ÿ���� ��������
        {
            Debug.Log("���ֿ�");
            myTarget = null;
            pickupActivated = false;
            actionText.gameObject.SetActive(false);
        }
    }
}
