using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;

    private bool pickupActivated = false; //습득 가능할 시 true

    [SerializeField]
    private LayerMask layerMask; //아이템 레이어만 발동
    private Transform myTarget;
    [SerializeField]
    private Inventory theInventory;

    [SerializeField]
    private TMP_Text actionText; //필요한 컴포넌트
    private GameObject obj;

    private void Start()
    {
        transform.parent = null;
        obj = Instantiate(Resources.Load("Prefabs/Item/ItemEffect") as GameObject);
        theInventory = SceneData.Inst.myinven;
        actionText = SceneData.Inst.actionText;
    }

    private void FixedUpdate()
    {
        obj.transform.position = transform.position;
    }

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log(item.itemName + " 획득했습니다");
                theInventory.AcquireItem(item);
                pickupActivated = false;
                actionText.gameObject.SetActive(false);
                SceneData.Inst.quest2.Check(); //이때 퀘스트 슬롯의 아이템 갯수를 늘려야함
                Destroy(gameObject);
                Destroy(obj);
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
            actionText.gameObject.SetActive(true);
            actionText.text = item.itemName + " 획득" + "<color=yellow>" + "(F)" + "</color>";
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
        //if (myTarget != null) return;
        if (myTarget == other.transform)//타겟이 빠져나감
        {
            Debug.Log("못주움");
            myTarget = null;
            pickupActivated = false;
            actionText.gameObject.SetActive(false);
        }
    }
}
