using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range; //습득 가능한 최대 거리
    private bool pickupActivated = false; //습득 가능할 시 true

    Collider col = null;

    [SerializeField]
    private LayerMask layerMask; //아이템 레이어만 발동
    private Transform myTarget;
    [SerializeField]
    private Inventory theInventory;

    [SerializeField]
    private TMP_Text actionText; //필요한 컴포넌트

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log(myTarget.GetComponent<ItemPickUp>().item.itemName + " 획득했습니다");
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
            actionText.text = other.transform.GetComponent<ItemPickUp>().item.itemName + " 획득" + "<color=yellow>" + "(F)" + "</color>";
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(myTarget == other.transform) //아직 있는 상태
        {
            CanPickUp();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (myTarget != null) return;
        if(myTarget == other.transform)//타겟이 빠져나감
        {
            Debug.Log("못주움");
            myTarget = null;
            pickupActivated = false;
            actionText.gameObject.SetActive(false);
        }
    }
}
