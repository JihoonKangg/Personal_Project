using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : UIchecker
{
    public static bool inventoryActivated = false;

    //�ʿ��� ������Ʈ
    [SerializeField]
    private GameObject go_inventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;

    //���Ե�
    public InventorySlot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        TryOpenInventory();
    }
    private void TryOpenInventory()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;
            if (inventoryActivated)
            {
                OpenInventory();
                Time.timeScale = 0.0f;
            }
            else
            {
                CloseInventory();
                Time.timeScale = 1.0f;
            }
        }
    }


    private void OpenInventory()
    {
        go_inventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        go_inventoryBase.SetActive(false);
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        if(Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].Additem(_item, _count);
                return;
            }
        }
    }

    public void Exit()
    {
        go_inventoryBase.SetActive(false);
        if (Time.timeScale == 1.0f) return;
        Time.timeScale = 1.0f;
    }
}
