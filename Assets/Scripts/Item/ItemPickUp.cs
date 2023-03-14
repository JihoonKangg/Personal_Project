using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;

    private bool pickupActivated = false; //���� ������ �� true

    [SerializeField]
    private LayerMask layerMask; //������ ���̾ �ߵ�
    private Transform myTarget;
    [SerializeField]
    private Inventory theInventory;

    [SerializeField]
    private TMP_Text actionText; //�ʿ��� ������Ʈ
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
                Debug.Log(item.itemName + " ȹ���߽��ϴ�");
                theInventory.AcquireItem(item);
                pickupActivated = false;
                actionText.gameObject.SetActive(false);
                SceneData.Inst.quest2.Check(); //�̶� ����Ʈ ������ ������ ������ �÷�����
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
            actionText.text = item.itemName + " ȹ��" + "<color=yellow>" + "(F)" + "</color>";
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (myTarget == other.transform) //���� �ִ� ����
        {
            CanPickUp();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (myTarget != null) return;
        if (myTarget == other.transform)//Ÿ���� ��������
        {
            Debug.Log("���ֿ�");
            myTarget = null;
            pickupActivated = false;
            actionText.gameObject.SetActive(false);
        }
    }
}
